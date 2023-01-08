using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model.DTO
{
    public class VillaCreateDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string ImageURl { get; set; }
        public string Details { get; set; }

        public string Amenity { get; set; }
        public int Occupancy { get; set; }
        public int Area { get; set; }
    }
}
