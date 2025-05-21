using FileManager.PL.Models;

namespace FileManager.PL.Services.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileItem>> GetFilesByFolderIdAsync(int folderId);
        Task<FileItem> GetFileByIdAsync(int id);
        Task<FileItem> UploadFileAsync(IFormFile file, int folderId, string description);
        Task DeleteFileAsync(int id);
        Task<(byte[] FileContents, string ContentType, string FileName)> DownloadFileAsync(int id);
    }
}
