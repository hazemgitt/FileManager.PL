using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FileManager.PL.Models
{
    public class Folder
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        public virtual ICollection<FileItem> Files { get; set; }

        public Folder()
        {
            Files = new HashSet<FileItem>();
        }
    }
}
