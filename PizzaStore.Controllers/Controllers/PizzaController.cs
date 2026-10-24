
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Controllers.Models;
using PizzaStore.Controllers.Services;

namespace PizzaStore.Controllers.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    // GET all pizzas
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();

    // PUT: Update a pizza
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = PizzaService.Get(id);

        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }

    // DELETE: Remove a pizza
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
    
    // POST: Create a pizza
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);

        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }


    // GET one pizza by Id
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }
}
