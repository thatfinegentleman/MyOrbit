using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class GroupService : IGroupService
{
    private readonly string _connectionString;

    public GroupService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"];
    }

    public async Task<IEnumerable<GroupDto>> GetAllAsync(string userId)
    {
        var sql = @"
            SELECT 
                Id, Name, ColorHex 
            FROM Groups 
            WHERE UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<GroupDto>(sql, new { UserId = userId });
    }

    public async Task<GroupDto?> GetByIdAsync(int id, string userId)
    {
        var sql = @"
            SELECT 
                Id, Name, ColorHex 
            FROM Groups 
            WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<GroupDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(Group group, string userId)
    {
        var sql = @"
            INSERT INTO Groups (Name, ColorHex, UserId) 
            VALUES (@Name, @ColorHex, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        group.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, group);
    }

    public async Task<bool> UpdateAsync(Group group, string userId)
    {
        var sql = @"
            UPDATE Groups SET Name = @Name, ColorHex = @ColorHex 
            WHERE Id = @Id AND UserId = @UserId";

        group.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, group);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM Groups WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}