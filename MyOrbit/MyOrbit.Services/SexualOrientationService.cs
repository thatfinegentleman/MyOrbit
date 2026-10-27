using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class SexualOrientationService : ISexualOrientationService
{
    private readonly string _connectionString;

    public SexualOrientationService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"]
            ?? throw new System.ArgumentNullException("Stringa di connessione mancante.");
    }

    public async Task<IEnumerable<SexualOrientationDto>> GetAllAsync(string userId)
    {
        var sql = "SELECT Id, Name FROM SexualOrientations WHERE UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<SexualOrientationDto>(sql, new { UserId = userId });
    }

    public async Task<SexualOrientationDto?> GetByIdAsync(int id, string userId)
    {
        var sql = "SELECT Id, Name FROM SexualOrientations WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<SexualOrientationDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(SexualOrientation sexualOrientation, string userId)
    {
        var sql = @"
            INSERT INTO SexualOrientations (Name, UserId) 
            VALUES (@Name, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        sexualOrientation.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, sexualOrientation);
    }

    public async Task<bool> UpdateAsync(SexualOrientation sexualOrientation, string userId)
    {
        var sql = "UPDATE SexualOrientations SET Name = @Name WHERE Id = @Id AND UserId = @UserId";
        sexualOrientation.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, sexualOrientation);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM SexualOrientations WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}