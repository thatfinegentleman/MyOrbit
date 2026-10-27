using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyOrbit.Dto;
using MyOrbit.Services.Interfaces;

namespace MyOrbit.Services;

public class NationalityService : INationalityService
{
    private readonly string _connectionString;

    public NationalityService(IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"]
            ?? throw new System.ArgumentNullException("stringa di connessione mancante");
    }

    public async Task<IEnumerable<NationalityDto>> GetAllAsync()
    {
        var sql = "SELECT * FROM Nationalities";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<NationalityDto>(sql);
    }

    public async Task<NationalityDto?> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM Nationalities WHERE Id = @Id";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<NationalityDto>(sql, new { Id = id });
    }
}