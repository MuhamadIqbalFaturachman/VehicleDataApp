using Microsoft.EntityFrameworkCore;
using VehicelDataApp.Models;

namespace VehicelDataApp.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}