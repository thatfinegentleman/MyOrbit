using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface IGenderService
{
    Task<IEnumerable<GenderDto>> GetAllAsync(string userId);
    Task<GenderDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(Gender gender, string userId);
    Task<bool> UpdateAsync(Gender gender, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}