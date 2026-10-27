using MyOrbit.Dto;

namespace MyOrbit.Services.Interfaces;

public interface IPersonPreferenceService
{
    Task<IEnumerable<PersonPreferenceDto>> GetPreferencesByPersonIdAsync(int personId, string userId);
    Task<bool> UpdatePersonPreferencesAsync(int personId, IEnumerable<PersonPreferenceDto> preferences, string userId);
}