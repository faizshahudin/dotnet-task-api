using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.models;

namespace TaskManagementAPI.Data {
    public class TaskDbContext: DbContext {
        public TaskDbContext(DbContextOptions<TaskDbContext> options): base(options) {
        }

        public DbSet<models.Task> Tasks { get; set; }
    }
}