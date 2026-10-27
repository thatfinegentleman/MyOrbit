using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface ISexualOrientationService
{
    Task<IEnumerable<SexualOrientationDto>> GetAllAsync(string userId);
    Task<SexualOrientationDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(SexualOrientation sexualOrientation, string userId);
    Task<bool> UpdateAsync(SexualOrientation sexualOrientation, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}