using Microsoft.EntityFrameworkCore;

namespace L05HandsOn.Models
{
    public class TasksContext: DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options): base(options) {

        }
        public DbSet<Task> Tasks {get; set;}
    }
}