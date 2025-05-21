using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileManager.PL.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public City()
        {
            Locations = new HashSet<Location>();
        }
    }
}