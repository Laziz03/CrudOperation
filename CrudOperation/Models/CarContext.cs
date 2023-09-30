using CrudOperation.Entyties;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Models
{
    public class CarContext:DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {

        }
        public  DbSet<Car> Cars { get; set; }


    }
}
