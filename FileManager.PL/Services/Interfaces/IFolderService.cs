using FileManager.PL.Models;

namespace FileManager.PL.Services.Interfaces
{
    public interface IFolderService
    {
        Task<IEnumerable<Folder>> GetFoldersByLocationIdAsync(int locationId);
        Task<Folder> GetFolderByIdAsync(int id);
        Task<Folder> CreateFolderAsync(Folder folder);
        Task UpdateFolderAsync(Folder folder);
        Task DeleteFolderAsync(int id);
    }
}
