using Microsoft.EntityFrameworkCore;

namespace C_BackendFinal.Models
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options): base(options){}

        public DbSet<Post> Posts {get; set;} 
    }
}