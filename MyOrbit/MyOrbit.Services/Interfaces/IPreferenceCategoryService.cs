using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface IPreferenceCategoryService
{
    Task<IEnumerable<PreferenceCategoryDto>> GetAllAsync(string userId);
    Task<PreferenceCategoryDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(PreferenceCategory preferenceCategory, string userId);
    Task<bool> UpdateAsync(PreferenceCategory preferenceCategory, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}