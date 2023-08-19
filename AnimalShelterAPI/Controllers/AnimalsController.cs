using Microsoft.AspNetCore.Mvc;
using AnimalShelterAPI.Models;

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

                AnimalData animals = new AnimalData { Cats = cats, Dogs = dogs };
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
        [HttpPost]
        public IActionResult Create(string animal, string name, string breed, int age)
        {
            if (string.IsNullOrEmpty(animal) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(breed) || age <= 0)
            {
                return BadRequest(new { error = true, message = "Invalid input data." });
            }

            bool isAdopted = false;

            if (animal.ToLower() == "cat")
            {
                var newCat = new Cat
                {
                    Name = name,
                    Breed = breed,
                    Age = age,
                    IsAdopted = isAdopted
                };
                _context.Cats.Add(newCat);
                _context.SaveChanges();

                return Ok(new { error = false, message = "Cat created successfully." });
            }
            else if (animal.ToLower() == "dog")
            {
                var newDog = new Dog
                {
                    Name = name,
                    Breed = breed,
                    Age = age,
                    IsAdopted = isAdopted
                };
                _context.Dogs.Add(newDog);
                _context.SaveChanges();

                return Ok(new { message = "Dog created successfully." });
            }
            else
            {
                return BadRequest(new { error = true, message = "Invalid Animal Type" });
            }
        }


    }
}
