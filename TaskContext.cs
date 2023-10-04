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
        List<Category> categoriesInit = new List<Category>();
        categoriesInit.Add(new Category()
        {
            CategoryId = Guid.Parse("89da336e-3532-45a8-832a-d8197825f16d"),
            Name = "Actividades Pendiente",
            Pound = 20,

        });
        categoriesInit.Add(new Category()
        {
            CategoryId = Guid.Parse("89da336e-3532-45a8-832a-d8197825f102"),
            Name = "Actividades Personales",
            Pound = 50,

        });

        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);
            category.Property(p => p.Name).IsRequired().HasMaxLength(150);
            category.Property(p => p.Description).IsRequired(false);
            category.Property(p => p.Pound);
            category.HasData(categoriesInit);

        });

        List <Task> tasksInit = new List<Task> ();
        tasksInit.Add(new Task(){
            TaskId = Guid.Parse("89da336e-3532-45a8-832a-d8197825f410"),
            CategoryId = Guid.Parse("89da336e-3532-45a8-832a-d8197825f102"),
            PriorityTask =Priority.Mid,
            Title = "Pago de servicios publicos"

              
        });
        tasksInit.Add(new Task(){
            TaskId = Guid.Parse("89da336e-3532-45a8-832a-d8197825f411"),
            CategoryId = Guid.Parse("89da336e-3532-45a8-832a-d8197825f16d"),
            PriorityTask =Priority.Low,
            Title = "Pago de servicios publicos"

              
        });
        modelBuilder.Entity<Task>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);
            task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
            task.Property(p => p.Title).IsRequired().HasMaxLength(150);
            task.Property(p => p.Description).IsRequired(false);
            task.Property(p => p.PriorityTask);
            task.Ignore(p => p.Resume);
            task.Property(p => p.InsertDate);
            task.Property(p => p.Category);
            task.HasData(tasksInit);

        });
        base.OnModelCreating(modelBuilder);
    }


}