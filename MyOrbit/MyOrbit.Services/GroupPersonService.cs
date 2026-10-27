using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class GroupPersonService : IGroupPersonService
{
    private readonly string _connectionString;

    public GroupPersonService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"];
    }

    public async Task<IEnumerable<GroupDto>> GetGroupsByPersonIdAsync(int personId, string userId)
    {
        var sql = @"
            SELECT g.Id, g.Name, g.ColorHex 
            FROM Groups g
            INNER JOIN GroupPerson gp ON g.Id = gp.GroupId
            INNER JOIN People p ON gp.PersonId = p.Id
            WHERE p.Id = @PersonId AND p.UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<GroupDto>(sql, new { PersonId = personId, UserId = userId });
    }

    public async Task<bool> UpdatePersonGroupsAsync(int personId, IEnumerable<int> groupIds, string userId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            var deleteSql = @"
                DELETE gp 
                FROM GroupPerson gp 
                INNER JOIN People p ON gp.PersonId = p.Id 
                WHERE gp.PersonId = @PersonId AND p.UserId = @UserId";

            await connection.ExecuteAsync(deleteSql, new { PersonId = personId, UserId = userId }, transaction);

            if (groupIds != null && groupIds.Any())
            {
                var insertSql = @"
                    INSERT INTO GroupPerson (GroupId, PersonId)
                    SELECT @GroupId, @PersonId
                    FROM People p
                    INNER JOIN Groups g ON g.Id = @GroupId
                    WHERE p.Id = @PersonId AND p.UserId = @UserId AND g.UserId = @UserId";

                var insertData = groupIds.Select(gId => new { GroupId = gId, PersonId = personId, UserId = userId });
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