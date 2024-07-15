using Microsoft.EntityFrameworkCore;
using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<BuildingConfiguration> BuildingConfigurations { get; set; }



    }
}
