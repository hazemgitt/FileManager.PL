using FileManager.PL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.PL.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IFolderService _folderService;

        public FilesController(IFileService fileService, IFolderService folderService)
        {
            _fileService = fileService;
            _folderService = folderService;
        }

        // GET: Files/Index/5 (folderId)
        public async Task<IActionResult> Index(int id)
        {
            var folder = await _folderService.GetFolderByIdAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            ViewBag.Folder = folder;
            var files = await _fileService.GetFilesByFolderIdAsync(id);
            return View(files);
        }

        // GET: Files/Upload/5 (folderId)
        public async Task<IActionResult> Upload(int id)
        {
            var folder = await _folderService.GetFolderByIdAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            ViewBag.Folder = folder;
            return View();
        }

        // POST: Files/Upload/5 (folderId)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int id, [Bind("Description")] string description)
        {
            var folder = await _folderService.GetFolderByIdAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            var file = Request.Form.Files.GetFile("file");
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Please select a file to upload");
                ViewBag.Folder = folder;
                return View();
            }

            await _fileService.UploadFileAsync(file, id, description);
            return RedirectToAction(nameof(Index), new { id });
        }

        // GET: Files/Download/5
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var (fileContents, contentType, fileName) = await _fileService.DownloadFileAsync(id);
                return File(fileContents, contentType, fileName);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Files/View/5
        public async Task<IActionResult> View(int id)
        {
            var file = await _fileService.GetFileByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            // For images and PDFs, we'll display them in the browser
            if (file.ContentType.StartsWith("image/") || file.ContentType == "application/pdf")
            {
                var (fileContents, contentType, _) = await _fileService.DownloadFileAsync(id);
                return File(fileContents, contentType);
            }

            // For other file types, we'll download them
            return RedirectToAction(nameof(Download), new { id });
        }

        // POST: Files/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int folderId)
        {
            await _fileService.DeleteFileAsync(id);
            return RedirectToAction(nameof(Index), new { id = folderId });
        }
    }
}
