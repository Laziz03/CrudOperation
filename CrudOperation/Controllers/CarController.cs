using CrudOperation.Entyties;
using CrudOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CrudOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarContext _dbContext;
        public CarController(CarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            if(_dbContext.Cars == null)
            {
                return NotFound();  
            }
            return await _dbContext.Cars.ToListAsync();
        }
        [HttpGet("GetCars/{id}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars(int id)

        {

            if (_dbContext.Cars == null)
            {
                return NotFound();
            }
            var car = await _dbContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
        [HttpPost] 
        public async Task<ActionResult<IEnumerable<Car>>> PostCars(Car car)
        {
            _dbContext.Cars.Add(car);
            await _dbContext.SaveChangesAsync();
             
            return CreatedAtAction(nameof(GetCars),new {id =  car.Id}, car);
        }
 
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Car>>> PutCars(int id,Car car)
        {
            if(id != car.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(car).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CarAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool CarAvailable(int id)
        {
            return (_dbContext.Cars?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async  Task<IActionResult>DeleteCars(int id)
        {
            if(_dbContext.Cars == null)
            {
                return NotFound();
            }
            var car = await _dbContext.Cars.FindAsync(id);
            if(car == null)
            {
                return NotFound();
            }
             _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}







