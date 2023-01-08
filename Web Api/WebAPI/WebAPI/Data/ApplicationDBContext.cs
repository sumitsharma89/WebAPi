using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {}
        
        public DbSet<Villa> Villas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(new Villa
            {
                Id = 1,
                Name = "House 1",
                Details = "House 1",
                ImageURl = "",
                Amenity = "",
                Occupancy = 5,
                Area = 500,
                CreatedDate = DateTime.Now,
                ModifiedDate= DateTime.Now

            },
            new Villa
            {
                Id = 2,
                Name = "House 2",
                Details = "House 2",
                ImageURl = "",
                Amenity = "",
                Occupancy = 10,
                Area = 1000,
                CreatedDate = DateTime.Now

            });
        }
    }
}
