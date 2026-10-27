using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class PersonPreferenceService : IPersonPreferenceService
{
    private readonly string _connectionString;

    public PersonPreferenceService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"];
    }

    public async Task<IEnumerable<PersonPreferenceDto>> GetPreferencesByPersonIdAsync(int personId, string userId)
    {
        var sql = @"
            SELECT 
                pp.PersonId, pp.PreferenceId, pp.Likes,
                pr.Name AS PreferenceName,
                pc.Name AS PreferenceCategoryName
            FROM PersonPreference pp
            INNER JOIN People p ON pp.PersonId = p.Id
            INNER JOIN Preferences pr ON pp.PreferenceId = pr.Id
            INNER JOIN PreferenceCategories pc ON pr.PreferenceCategoryId = pc.Id
            WHERE p.Id = @PersonId AND p.UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<PersonPreferenceDto>(sql, new { PersonId = personId, UserId = userId });
    }

    public async Task<bool> UpdatePersonPreferencesAsync(int personId, IEnumerable<PersonPreferenceDto> preferences, string userId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            var deleteSql = @"
                DELETE pp 
                FROM PersonPreference pp 
                INNER JOIN People p ON pp.PersonId = p.Id 
                WHERE pp.PersonId = @PersonId AND p.UserId = @UserId";

            await connection.ExecuteAsync(deleteSql, new { PersonId = personId, UserId = userId }, transaction);

            if (preferences != null && preferences.Any())
            {
                var insertSql = @"
                    INSERT INTO PersonPreference (PreferenceId, PersonId, Likes)
                    SELECT @PreferenceId, @PersonId, @Likes
                    FROM People p
                    INNER JOIN Preferences pr ON pr.Id = @PreferenceId
                    WHERE p.Id = @PersonId AND p.UserId = @UserId AND pr.UserId = @UserId";

                var insertData = preferences.Select(pref => new
                {
                    PreferenceId = pref.PreferenceId,
                    PersonId = personId,
                    Likes = pref.Likes,
                    UserId = userId
                });
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