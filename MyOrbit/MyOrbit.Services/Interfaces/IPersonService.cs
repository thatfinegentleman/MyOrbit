using MyOrbit.Dto;
using MyOrbit.Models;

namespace MyOrbit.Services.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetAllAsync(string userId);
    Task<PersonDto?> GetByIdAsync(int id, string userId);
    Task<int> CreateAsync(Person person, string userId);
    Task<bool> UpdateAsync(Person person, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}