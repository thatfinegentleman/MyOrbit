using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface ISchoolService
{
    Task<IEnumerable<SchoolDto>> GetAllAsync(string userId);
    Task<SchoolDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(School school, string userId);
    Task<bool> UpdateAsync(School school, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}