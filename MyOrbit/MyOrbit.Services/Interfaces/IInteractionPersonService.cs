using MyOrbit.Dto;

namespace MyOrbit.Services.Interfaces;

public interface IInteractionPersonService
{
    Task<IEnumerable<InteractionDto>> GetInteractionsByPersonIdAsync(int personId, string userId);
    Task<bool> UpdatePersonInteractionsAsync(int personId, IEnumerable<int> interactionIds, string userId);
}