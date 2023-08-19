using Microsoft.AspNetCore.Mvc;
using AnimalShelterAPI.Models;
using System.Linq;

namespace AnimalShelterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalShelterDbContext _context;

        public AnimalsController(AnimalShelterDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string? Animal, int? Id, string? Breed, int? Age, string? IsAdopted)
        {

            if (Animal == null)
            {
                Animal = "both";
            }

            if (Animal == "both")
            {
                List<Cat> cats = _context.Cats.ToList();
                List<Dog> dogs = _context.Dogs.ToList();

                var animals = new { Cats = cats, Dogs = dogs };
                return Ok(animals);
            }
            else if (Animal == "cat")
            {
                var cats = _context.Cats.ToList();


                return Ok(cats);

            }
            else if (Animal == "dog")
            {
                var dogs = _context.Dogs.ToList();

                return Ok(dogs);
            }
            else
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Invalid Animal Type"
                });
            }

        }
    }
}
