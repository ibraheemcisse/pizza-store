using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

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

// Temporary sample data
var pizzas = new List<Pizza>
{
    new(1, "Margherita"),
    new(2, "Pepperoni"),
    new(3, "Hawaiian")
};

// Home endpoint
app.MapGet("/", () => "Hello World!");

// Get all pizzas
app.MapGet("/pizzas", () => pizzas);

// Get one pizza by ID
app.MapGet("/pizzas/{id}", (int id) =>
{
    var pizza = pizzas.FirstOrDefault(p => p.Id == id);

    return pizza is not null
        ? Results.Ok(pizza)
        : Results.NotFound();
});

app.MapPost("/pizzas", (Pizza pizza) =>
{
    pizza = pizza with { Id = pizzas.Count + 1 };

    pizzas.Add(pizza);

    return Results.Created($"/pizzas/{pizza.Id}", pizza);
});


app.MapPut("/pizzas/{id}", (int id, Pizza updatedPizza) =>
{
    var index = pizzas.FindIndex(p => p.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    pizzas[index] = updatedPizza with { Id = id };

    return Results.NoContent();
});

app.MapDelete("/pizzas/{id}", (int id) =>
{
    var pizza = pizzas.FirstOrDefault(p => p.Id == id);

    if (pizza is null)
    {
        return Results.NotFound();
    }

    pizzas.Remove(pizza);

    return Results.NoContent();
});

app.Run();
