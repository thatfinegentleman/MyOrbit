using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class GenderService : IGenderService
{
    private readonly string _connectionString;

    public GenderService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"]
            ?? throw new System.ArgumentNullException("Stringa di connessione mancante.");
    }

    public async Task<IEnumerable<GenderDto>> GetAllAsync(string userId)
    {
        var sql = "SELECT Id, Name FROM Genders WHERE UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<GenderDto>(sql, new { UserId = userId });
    }

    public async Task<GenderDto?> GetByIdAsync(int id, string userId)
    {
        var sql = "SELECT Id, Name FROM Genders WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<GenderDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(Gender gender, string userId)
    {
        var sql = @"
            INSERT INTO Genders (Name, UserId) 
            VALUES (@Name, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        gender.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, gender);
    }

    public async Task<bool> UpdateAsync(Gender gender, string userId)
    {
        var sql = "UPDATE Genders SET Name = @Name WHERE Id = @Id AND UserId = @UserId";
        gender.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, gender);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM Genders WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}