using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCarsales.DataStore;
using MiniCarsales.Models;

namespace MiniCarsales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
            if (!_context.Car.Any())
            {
                AddInitialData(context);
            }
        }

        private void AddInitialData(ApplicationDbContext context)
        {
            context.Car.Add(new Car
            {
                Type = nameof(Car),
                Make = "Mazda",
                Model = "6",
                Engine = "Diesel",
                Doors = 4,
                Wheels = 4,
                BodyType = "Sedan"
            });
            context.Car.Add(new Car
            {
                Type = nameof(Car),
                Make = "Toyota",
                Model = "RAV4",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "SUV"
            });
            context.Car.Add(new Car
            {
                Type = nameof(Car),
                Make = "Audi",
                Model = "A5",
                Engine = "Petrol",
                Doors = 4,
                Wheels = 4,
                BodyType = "Hatch"
            });
            context.SaveChanges();
        }

        // GET: api/Cars
        // List all cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> ListCars()
        {
            return await _context.Car.ToListAsync();
        }

        // GET: api/Cars/5
        // Get a specific car
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // POST: api/Cars
        // Create a new car
        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // PUT: api/Cars/5
        // Update a specific car
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar([FromRoute] int id, [FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Cars/5
        // Delete a specific car
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return Ok(car);
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}