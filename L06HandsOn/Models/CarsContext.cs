using L06Hands_On.Models;
using Microsoft.EntityFrameworkCore;

namespace L06Hands_On.Models
{
    public class CarsContext : DbContext {
        public CarsContext(DbContextOptions<CarsContext> options) : base(options) {

        }
        public DbSet<Car> Cars { get; set; }
    }
}