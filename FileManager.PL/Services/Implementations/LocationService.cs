using FileManager.PL.Data.Repositories;
using FileManager.PL.Models;
using FileManager.PL.Services.Interfaces;

namespace FileManager.PL.Services.Implementations
{
    public class LocationService: ILocationService
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _locationRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Location>> GetLocationsByCityIdAsync(int cityId)
        {
            return await _locationRepository.FindAsync(l => l.CityId == cityId);
        }

        public async Task<Location> GetLocationByIdAsync(int id)
        {
            return await _locationRepository.GetByIdAsync(id);
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            try
            {
                // Add the new location to the repository
                await _locationRepository.AddAsync(location);

                // Save changes to persist the new entity
                await _locationRepository.SaveChangesAsync();

                // Return the updated entity (with ID if auto-generated)
                return location;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating location: {ex.Message}");
                throw; // Re-throw to allow controller to handle errors
            }
        }

        public async Task UpdateLocationAsync(Location location)
        {
            await _locationRepository.UpdateAsync(location);
            await _locationRepository.SaveChangesAsync();
        }

        public async Task DeleteLocationAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location != null)
            {
                await _locationRepository.DeleteAsync(location);
                await _locationRepository.SaveChangesAsync();
            }
        }
    }
}
