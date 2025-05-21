using FileManager.PL.Data.Repositories;
using FileManager.PL.Models;
using FileManager.PL.Services.Interfaces;

namespace FileManager.PL.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IRepository<FileItem> _fileRepository;
        private readonly IRepository<Folder> _folderRepository;
        private readonly IWebHostEnvironment _environment;

        public FileService(
            IRepository<FileItem> fileRepository,
            IRepository<Folder> folderRepository,
            IWebHostEnvironment environment)
        {
            _fileRepository = fileRepository;
            _folderRepository = folderRepository;
            _environment = environment;
        }

        public async Task<IEnumerable<FileItem>> GetFilesByFolderIdAsync(int folderId)
        {
            return await _fileRepository.FindAsync(f => f.FolderId == folderId);
        }

        public async Task<FileItem> GetFileByIdAsync(int id)
        {
            return await _fileRepository.GetByIdAsync(id);
        }

        public async Task<FileItem> UploadFileAsync(IFormFile file, int folderId, string description)
        {
            var folder = await _folderRepository.GetByIdAsync(folderId);
            if (folder == null)
                throw new ArgumentException("Folder not found", nameof(folderId));

            // Create directory structure: uploads/cityId/locationId/folderId/
            var location = folder.Location;
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads",
                location.CityId.ToString(),
                location.Id.ToString(),
                folderId.ToString());

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Create unique filename
            var fileName = Path.GetFileName(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create file record
            var fileItem = new FileItem
            {
                FileName = fileName,
                ContentType = file.ContentType,
                Description = description,
                StoragePath = filePath,
                FileSize = file.Length,
                UploadDate = DateTime.UtcNow,
                FolderId = folderId
            };

            await _fileRepository.AddAsync(fileItem);
            await _fileRepository.SaveChangesAsync();

            return fileItem;
        }

        public async Task DeleteFileAsync(int id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file != null)
            {
                // Delete physical file
                if (File.Exists(file.StoragePath))
                {
                    File.Delete(file.StoragePath);
                }

                // Delete database record
                await _fileRepository.DeleteAsync(file);
                await _fileRepository.SaveChangesAsync();
            }
        }

        public async Task<(byte[] FileContents, string ContentType, string FileName)> DownloadFileAsync(int id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null)
                throw new ArgumentException("File not found", nameof(id));

            if (!File.Exists(file.StoragePath))
                throw new FileNotFoundException("Physical file not found", file.StoragePath);

            var fileBytes = await File.ReadAllBytesAsync(file.StoragePath);
            return (fileBytes, file.ContentType, file.FileName);
        }
    }
}
