using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FileManager.PL.Models
{
    public class FileItem
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string FileName { get; set; }

        [Required, StringLength(100)]
        public string ContentType { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string StoragePath { get; set; }

        public long FileSize { get; set; }

        public DateTime UploadDate { get; set; }

        public int FolderId { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }
    }

}
