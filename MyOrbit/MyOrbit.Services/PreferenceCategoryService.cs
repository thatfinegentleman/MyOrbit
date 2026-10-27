using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class PreferenceCategoryService : IPreferenceCategoryService
{
    private readonly string _connectionString;

    public PreferenceCategoryService (IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"]
            ?? throw new System.ArgumentNullException("Stringa di connessione mancante.");
    }

    public async Task<IEnumerable<PreferenceCategoryDto>> GetAllAsync(string userId)
    {
        var sql = "SELECT Id, Name FROM PreferenceCategories WHERE UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<PreferenceCategoryDto>(sql, new { UserId = userId });
    }

    public async Task<PreferenceCategoryDto?> GetByIdAsync(int id, string userId)
    {
        var sql = "SELECT Id, Name FROM PreferenceCategories WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<PreferenceCategoryDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(PreferenceCategory preferenceCategory, string userId)
    {
        var sql = @"
            INSERT INTO PreferenceCategories (Name, UserId) 
            VALUES (@Name, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        preferenceCategory.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, preferenceCategory);
    }

    public async Task<bool> UpdateAsync(PreferenceCategory preferenceCategory, string userId)
    {
        var sql = "UPDATE PreferenceCategories SET Name = @Name WHERE Id = @Id AND UserId = @UserId";
        preferenceCategory.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, preferenceCategory);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM PreferenceCategories WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}