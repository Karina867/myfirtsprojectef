using Microsoft.EntityFrameworkCore;
using mywebapp.Models;
using Task = mywebapp.Models.Task;

namespace projectef;

public class TasksContext : DbContext
{

    public DbSet<Category> Categorys { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

    // Implementation of Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);
            category.Property(p => p.Name).IsRequired().HasMaxLength(150);
            category.Property(p => p.Description);
            category.Property(p => p.Pound);

        });
        modelBuilder.Entity<Task>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);
            task.HasOne(p => p.Category).WithMany(p=>p.Tasks).HasForeignKey(p=>p.CategoryId);
            task.Property(p => p.Title).IsRequired().HasMaxLength(150);
            task.Property(p => p.Description);
            task.Property(p => p.PriorityTask);
            task.Property(p => p.Resume);
            task.Property(p => p.InsertDate);
            task.Ignore(p => p.Category);

        });
        base.OnModelCreating(modelBuilder);
    }


}