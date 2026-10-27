using MyOrbit.Dto;

namespace MyOrbit.Services.Interfaces;

public interface INationalityService
{
    Task<IEnumerable<NationalityDto>> GetAllAsync();
    Task<NationalityDto?> GetByIdAsync(int id);
}