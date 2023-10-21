using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;
using mywebapp.Models;

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
app.MapGet("/api/tasks", ([FromServices] TasksContext dbContext) =>
{
    return Results.Ok(dbContext.Tasks.Include(p => p.Category));
});

app.MapPost("api/tasks", async ([FromServices] TasksContext dbContext, [FromBody] Tasks task) =>
{
    task.TaskId = Guid.NewGuid();
    task.InsertDate = DateTime.Now;
    await dbContext.AddAsync(task);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});


app.MapPut("api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromBody] Tasks task, [FromRoute] Guid id) =>
{

    var actualTask = dbContext.Tasks.Find(id);
    if (actualTask != null)
    {
        actualTask.CategoryId = task.CategoryId;
        actualTask.Title = task.Title;
        actualTask.PriorityTask = task.PriorityTask;
        actualTask.Description = task.Description;

        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();

});
app.MapDelete("api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) =>
{
    var actualTask = dbContext.Tasks.Find(id);

    if (actualTask != null)
    {
    Console.WriteLine(actualTask.Title);
        dbContext.Remove(id);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

app.Run();
