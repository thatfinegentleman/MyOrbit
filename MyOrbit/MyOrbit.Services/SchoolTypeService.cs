using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class SchoolTypeService : ISchoolTypeService
{
    private readonly string _connectionString;

    public SchoolTypeService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"]
            ?? throw new System.ArgumentNullException("Stringa di connessione mancante.");
    }

    public async Task<IEnumerable<SchoolTypeDto>> GetAllAsync(string userId)
    {
        var sql = "SELECT Id, Name FROM SchoolTypes WHERE UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<SchoolTypeDto>(sql, new { UserId = userId });
    }

    public async Task<SchoolTypeDto?> GetByIdAsync(int id, string userId)
    {
        var sql = "SELECT Id, Name FROM SchoolTypes WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<SchoolTypeDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(SchoolType schoolType, string userId)
    {
        var sql = @"
            INSERT INTO SchoolTypes (Name, UserId) 
            VALUES (@Name, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        schoolType.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, schoolType);
    }

    public async Task<bool> UpdateAsync(SchoolType schoolType, string userId)
    {
        var sql = "UPDATE SchoolTypes SET Name = @Name WHERE Id = @Id AND UserId = @UserId";
        schoolType.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, schoolType);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM SchoolTypes WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}