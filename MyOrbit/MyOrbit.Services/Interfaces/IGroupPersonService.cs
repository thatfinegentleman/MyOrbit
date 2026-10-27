using MyOrbit.Dto;

namespace MyOrbit.Services.Interfaces;

public interface IGroupPersonService
{
    Task<IEnumerable<GroupDto>> GetGroupsByPersonIdAsync(int personId, string userId);
    Task<bool> UpdatePersonGroupsAsync(int personId, IEnumerable<int> groupIds, string userId);
}