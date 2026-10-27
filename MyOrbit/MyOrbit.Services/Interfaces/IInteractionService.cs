using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface IInteractionService
{
    Task<IEnumerable<InteractionDto>> GetAllAsync(string userId);
    Task<InteractionDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(Interaction interaction, string userId);
    Task<bool> UpdateAsync(Interaction interaction, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}