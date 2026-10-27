using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class InteractionService : IInteractionService
{
    private readonly string _connectionString;

    public InteractionService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"];
    }

    public async Task<IEnumerable<InteractionDto>> GetAllAsync(string userId)
    {
        var sql = @"
            SELECT 
                Id, Title, Date, Description, IsPositive 
            FROM Interactions 
            WHERE UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<InteractionDto>(sql, new { UserId = userId });
    }

    public async Task<InteractionDto?> GetByIdAsync(int id, string userId)
    {
        var sql = @"
            SELECT 
                Id, Title, Date, Description, IsPositive 
            FROM Interactions 
            WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<InteractionDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(Interaction interaction, string userId)
    {
        var sql = @"
            INSERT INTO Interactions (Title, Date, Description, IsPositive, UserId) 
            VALUES (@Title, @Date, @Description, @IsPositive, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        interaction.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, interaction);
    }

    public async Task<bool> UpdateAsync(Interaction interaction, string userId)
    {
        var sql = @"
            UPDATE Interactions SET 
                Title = @Title, Date = @Date, Description = @Description, IsPositive = @IsPositive
            WHERE Id = @Id AND UserId = @UserId";

        interaction.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, interaction);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM Interactions WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}