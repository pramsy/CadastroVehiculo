using CadastroVehiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroVehiculo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Gearbox> Gearbox { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Vehicle> Vehicules { get; set; }

    }
}
