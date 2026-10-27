using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface IGroupService
{
    Task<IEnumerable<GroupDto>> GetAllAsync(string userId);
    Task<GroupDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(Group group, string userId);
    Task<bool> UpdateAsync(Group group, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}