using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class InteractionPersonService : IInteractionPersonService
{
    private readonly string _connectionString;

    public InteractionPersonService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"];
    }

    public async Task<IEnumerable<InteractionDto>> GetInteractionsByPersonIdAsync(int personId, string userId)
    {
        var sql = @"
            SELECT i.Id, i.Title, i.Date, i.Description, i.IsPositive 
            FROM Interactions i
            INNER JOIN InteractionPerson ip ON i.Id = ip.InteractionId
            INNER JOIN People p ON ip.PersonId = p.Id
            WHERE p.Id = @PersonId AND p.UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<InteractionDto>(sql, new { PersonId = personId, UserId = userId });
    }

    public async Task<bool> UpdatePersonInteractionsAsync(int personId, IEnumerable<int> interactionIds, string userId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            var deleteSql = @"
                DELETE ip 
                FROM InteractionPerson ip 
                INNER JOIN People p ON ip.PersonId = p.Id 
                WHERE ip.PersonId = @PersonId AND p.UserId = @UserId";

            await connection.ExecuteAsync(deleteSql, new { PersonId = personId, UserId = userId }, transaction);

            if (interactionIds != null && interactionIds.Any())
            {
                var insertSql = @"
                    INSERT INTO InteractionPerson (InteractionId, PersonId)
                    SELECT @InteractionId, @PersonId
                    FROM People p
                    INNER JOIN Interactions i ON i.Id = @InteractionId
                    WHERE p.Id = @PersonId AND p.UserId = @UserId AND i.UserId = @UserId";

                var insertData = interactionIds.Select(iId => new { InteractionId = iId, PersonId = personId, UserId = userId });
                await connection.ExecuteAsync(insertSql, insertData, transaction);
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}