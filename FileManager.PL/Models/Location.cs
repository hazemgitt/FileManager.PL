using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NuGet.Packaging.PackagingConstants;

namespace FileManager.PL.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string ?Description { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<Folder> Folders { get; set; }

        public Location()
        {
            Folders = new HashSet<Folder>();
        }
    }
}
