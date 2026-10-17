
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
var pizzas = new[]
{
    new { Id = 1, Name = "Margherita" },
    new { Id = 2, Name = "Pepperoni" },
    new { Id = 3, Name = "Hawaiian" }
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

app.Run();
