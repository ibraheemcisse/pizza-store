using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<PizzaDb>(options =>
    options.UseInMemoryDatabase("items"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PizzaStore API",
        Description = "Making the pizzas you love",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}


// Home endpoint
app.MapGet("/", () => "Hello World!");

// Get all pizzas

app.MapGet("/pizzas", async (PizzaDb db) =>
    await db.Pizzas.ToListAsync());

// Get one pizza by ID

app.MapGet("/pizzas/{id}", async (int id, PizzaDb db) =>
{
    var pizza = await db.Pizzas.FindAsync(id);

    return pizza is not null
        ? Results.Ok(pizza)
        : Results.NotFound();
});



app.MapPost("/pizzas", async (Pizza pizza, PizzaDb db) =>
{
    db.Pizzas.Add(pizza);
    await db.SaveChangesAsync();

    return Results.Created($"/pizzas/{pizza.Id}", pizza);
});


app.MapPut("/pizzas/{id}", async (int id, Pizza updatedPizza, PizzaDb db) =>
{
    var pizza = await db.Pizzas.FindAsync(id);

    if (pizza is null)
    {
        return Results.NotFound();
    }

    pizza.Name = updatedPizza.Name;
    pizza.Description = updatedPizza.Description;

    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("/pizzas/{id}", async (int id, PizzaDb db) =>
{
    var pizza = await db.Pizzas.FindAsync(id);

    if (pizza is null)
    {
        return Results.NotFound();
    }

    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();