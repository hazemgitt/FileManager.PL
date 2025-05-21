using FileManager.PL.Models;

namespace FileManager.PL.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City> GetCityByIdAsync(int id);
        Task<City> CreateCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task DeleteCityAsync(int id);
    }
}
