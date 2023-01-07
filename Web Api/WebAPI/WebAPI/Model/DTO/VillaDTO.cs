﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Area { get; set; }
    }
}
