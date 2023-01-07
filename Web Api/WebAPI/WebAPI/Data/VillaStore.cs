using WebAPI.Model.DTO;

namespace WebAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> Villas = new List<VillaDTO>
        {
            new VillaDTO{Id=1, Name="Bunglow", Area = 2000, Occupancy = 4  },
            new VillaDTO{Id=2, Name="Beach House", Area = 10000, Occupancy = 10    }
        };
    }
}
