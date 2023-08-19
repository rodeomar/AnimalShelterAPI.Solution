using Microsoft.AspNetCore.Mvc;
using AnimalShelterAPI.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            if (Animal.ToLower() == "both")
            {
                List<Cat> cats = _context.Cats.ToList();
                List<Dog> dogs = _context.Dogs.ToList();

                AnimalData animals = new AnimalData{ Cats = cats, Dogs = dogs };
                animals.Filter(Animal, Id, Breed, Age, IsAdopted);
                return Ok(animals);
            }
            else if (Animal.ToLower() == "cat")
            {
                var cats = _context.Cats.ToList();


                return Ok(cats);

            }
            else if (Animal.ToLower() == "dog")
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
