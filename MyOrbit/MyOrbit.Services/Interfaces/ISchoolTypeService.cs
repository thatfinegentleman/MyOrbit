using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface ISchoolTypeService
{
    Task<IEnumerable<SchoolTypeDto>> GetAllAsync(string userId);
    Task<SchoolTypeDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(SchoolType schoolType, string userId);
    Task<bool> UpdateAsync(SchoolType schoolType, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}