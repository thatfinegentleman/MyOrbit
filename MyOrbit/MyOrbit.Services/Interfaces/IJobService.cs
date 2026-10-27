using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface IJobService
{
    Task<IEnumerable<JobDto>> GetAllAsync(string userId);
    Task<JobDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(Job job, string userId);
    Task<bool> UpdateAsync(Job job, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}