using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projectef;

var builder = WebApplication.CreateBuilder(args);

// string path = "Data Source=JENNIFERKAR1531\\SQLEXPRESS; Initial Catalog= TareasDb; user id= kcruz; password= dev;";
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("cnTask"));
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGet("/dbconexion", ([FromServices] TasksContext dbContext) =>
                {
                    dbContext.Database.EnsureCreated();

                    return Results.Ok($"Base de datos en memoria: {dbContext.Database.IsInMemory()}");
                });
app.MapGet("/api/tasks", async ([FromServices] TasksContext dbContext) =>{
    return Results.Ok(dbContext.Tasks.Include(p => p.Category).Where(p => p.PriorityTask == mywebapp.Models.Priority.Low ));
});

app.Run();
