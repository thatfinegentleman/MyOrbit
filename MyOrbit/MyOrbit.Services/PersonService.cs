using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class PersonService : IPersonService
{
    private readonly string _connectionString;

    public PersonService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"]
            ?? throw new System.ArgumentNullException("La stringa di connessione è mancante.");
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync(string userId)
    {
        var sql = @"
            SELECT 
                p.*,
                g.Name AS GenderName,
                s.Name AS SexualOrientationName,
                n.Name AS NationalityName,
                n.Flag AS NationalityFlag,
                r.Name AS RelationshipName,
                r.ColorHex AS RelationshipColorHex,
                sch.Name AS SchoolName,
                j.Role AS JobRole,
                j.WorkplaceName AS JobWorkplaceName
            FROM People p
            INNER JOIN Genders g ON p.GenderId = g.Id
            INNER JOIN SexualOrientations s ON p.SexualOrientationId = s.Id
            INNER JOIN Relationships r ON p.RelationshipId = r.Id
            LEFT JOIN Nationalities n ON p.NationalityId = n.Id
            LEFT JOIN Schools sch ON p.SchoolId = sch.Id
            LEFT JOIN Jobs j ON p.JobId = j.Id
            WHERE p.UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<PersonDto>(sql, new { UserId = userId });
    }

    public async Task<PersonDto?> GetByIdAsync(int id, string userId)
    {
        var sql = @"
            SELECT 
                p.*,
                g.Name AS GenderName,
                s.Name AS SexualOrientationName,
                n.Name AS NationalityName,
                n.Flag AS NationalityFlag,
                r.Name AS RelationshipName,
                r.ColorHex AS RelationshipColorHex,
                sch.Name AS SchoolName,
                j.Role AS JobRole,
                j.WorkplaceName AS JobWorkplaceName
            FROM People p
            INNER JOIN Genders g ON p.GenderId = g.Id
            INNER JOIN SexualOrientations s ON p.SexualOrientationId = s.Id
            INNER JOIN Relationships r ON p.RelationshipId = r.Id
            LEFT JOIN Nationalities n ON p.NationalityId = n.Id
            LEFT JOIN Schools sch ON p.SchoolId = sch.Id
            LEFT JOIN Jobs j ON p.JobId = j.Id
            WHERE p.Id = @Id AND p.UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<PersonDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(Person person, string userId)
    {
        var sql = @"
            INSERT INTO People (
                Name, Surname, Nickname, BirthDate, BirthYear, ProfilePicture, 
                GenderId, SexualOrientationId, NationalityId, HomeAddress1, HomeAddress2, 
                RelationshipId, SchoolId, JobId, Notes, ProfileClicks, UserId
            ) 
            VALUES (
                @Name, @Surname, @Nickname, @BirthDate, @BirthYear, @ProfilePicture, 
                @GenderId, @SexualOrientationId, @NationalityId, @HomeAddress1, @HomeAddress2, 
                @RelationshipId, @SchoolId, @JobId, @Notes, @ProfileClicks, @UserId
            );
            SELECT CAST(SCOPE_IDENTITY() as int);";

        person.UserId = userId;

        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, person);
    }

    public async Task<bool> UpdateAsync(Person person, string userId)
    {
        var sql = @"
            UPDATE People SET 
                Name = @Name, Surname = @Surname, Nickname = @Nickname, 
                BirthDate = @BirthDate, BirthYear = @BirthYear, ProfilePicture = @ProfilePicture, 
                GenderId = @GenderId, SexualOrientationId = @SexualOrientationId, 
                NationalityId = @NationalityId, HomeAddress1 = @HomeAddress1, HomeAddress2 = @HomeAddress2, 
                RelationshipId = @RelationshipId, SchoolId = @SchoolId, JobId = @JobId, 
                Notes = @Notes, ProfileClicks = @ProfileClicks
            WHERE Id = @Id AND UserId = @UserId";

        person.UserId = userId;

        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, person);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM People WHERE Id = @Id AND UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}