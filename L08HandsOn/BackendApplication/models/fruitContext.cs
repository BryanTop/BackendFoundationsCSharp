using Microsoft.EntityFrameworkCore;

namespace BackendApplication.models
{
    public class FruitContext: DbContext 
    {
        public FruitContext(DbContextOptions<FruitContext> options): base(options)  {}

        public DbSet<Fruit> Fruits {get; set;}

        public DbSet<DeletedFruit> DeletedFruits {get; set;}

    }
}