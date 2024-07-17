using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("ToDoDb");
}); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/todoitems", async (AppDbContext dbContext) =>
{
    var items = await dbContext.TodoItems.ToListAsync();
    return Results.Ok(items);
});

app.MapGet("/api/todoitems/{id}", async (AppDbContext dbContext, int id) =>
{
    var item = await dbContext.TodoItems.FindAsync(id);
    if (item is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(item);
});

app.MapPost("/api/todoitems", async (AppDbContext dbContext, ToDoItem item) =>
{
    await dbContext.TodoItems.AddAsync(item);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/api/todoitems/{item.Id}", item);
});

app.Run();