using Microsoft.EntityFrameworkCore;
using MiniCarsales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCarsales.DataStore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Car { get; set; }
    }
}
