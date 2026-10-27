using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Models;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class JobService : IJobService
{
    private readonly string _connectionString;

    public JobService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"]
            ?? throw new System.ArgumentNullException("Stringa di connessione mancante.");
    }

    public async Task<IEnumerable<JobDto>> GetAllAsync(string userId)
    {
        var sql = @"
            SELECT 
                Id, Role, WorkplaceName, Location 
            FROM Jobs 
            WHERE UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<JobDto>(sql, new { UserId = userId });
    }

    public async Task<JobDto?> GetByIdAsync(int id, string userId)
    {
        var sql = @"
            SELECT 
                Id, Role, WorkplaceName, Location 
            FROM Jobs 
            WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<JobDto>(sql, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(Job job, string userId)
    {
        var sql = @"
            INSERT INTO Jobs (Role, WorkplaceName, Location, UserId) 
            VALUES (@Role, @WorkplaceName, @Location, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        job.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, job);
    }

    public async Task<bool> UpdateAsync(Job job, string userId)
    {
        var sql = @"
            UPDATE Jobs SET 
                Role = @Role, WorkplaceName = @WorkplaceName, Location = @Location
            WHERE Id = @Id AND UserId = @UserId";

        job.UserId = userId;
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, job);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var sql = "DELETE FROM Jobs WHERE Id = @Id AND UserId = @UserId";
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
        return rowsAffected > 0;
    }
}