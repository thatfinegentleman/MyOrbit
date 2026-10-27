using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class SchoolService : ISchoolService
{
    private readonly string _connectionString;

    public SchoolService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"];
    }

    public async Task<IEnumerable<SchoolDto>> GetAllAsync(string userId)
    {
        var sql = @"
            SELECT 
                s.Id, s.Name, s.SchoolTypeId, s.FieldOfStudy, s.Location,
                st.Name AS SchoolTypeName
            FROM Schools s
            INNER JOIN SchoolTypes st ON s.SchoolTypeId = st.Id
            WHERE s.UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<SchoolDto>(sql, new { UserId = userId });
    }

    public async Task<SchoolDto?> GetByIdAsync(int id, string userId)
    {
        var sql = @"
            SELECT 
                s.Id, s.Name, s.SchoolTypeId, s.FieldOfStudy, s.Location,
                st.Name AS SchoolTypeName
            FROM Schools s
            INNER JOIN SchoolTypes st ON s.SchoolTypeId = st.Id
            WHERE s.Id = @Id AND s.UserId = @UserId";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<SchoolDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(School school, string userId)
    {
        var sql = @"
            INSERT INTO Schools (Name, SchoolTypeId, FieldOfStudy, Location, UserId) 
            VALUES (@Name, @SchoolTypeId, @FieldOfStudy, @Location, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        school.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, school);
    }

    public async Task<bool> UpdateAsync(School school, string userId)
    {
        var sql = @"
            UPDATE Schools SET 
                Name = @Name, SchoolTypeId = @SchoolTypeId, 
                FieldOfStudy = @FieldOfStudy, Location = @Location
            WHERE Id = @Id AND UserId = @UserId";

        school.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, school);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM Schools WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}