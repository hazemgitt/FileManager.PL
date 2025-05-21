using FileManager.PL.Data.Repositories;
using FileManager.PL.Models;
using FileManager.PL.Services.Interfaces;

namespace FileManager.PL.Services.Implementations
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task<City> CreateCityAsync(City city)
        {
            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveChangesAsync();
            return city;
        }

        public async Task UpdateCityAsync(City city)
        {
            await _cityRepository.UpdateAsync(city);
            await _cityRepository.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if (city != null)
            {
                await _cityRepository.DeleteAsync(city);
                await _cityRepository.SaveChangesAsync();
            }
        }
    }
}
