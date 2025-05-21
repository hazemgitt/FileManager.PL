using FileManager.PL.Models;

namespace FileManager.PL.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<IEnumerable<Location>> GetLocationsByCityIdAsync(int cityId);
        Task<Location> GetLocationByIdAsync(int id);
        Task<Location> CreateLocationAsync(Location location);
        Task UpdateLocationAsync(Location location);
        Task DeleteLocationAsync(int id);
    }
}
