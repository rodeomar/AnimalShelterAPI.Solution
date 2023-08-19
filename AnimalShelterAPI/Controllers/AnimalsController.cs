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
                AnimalData cats = new AnimalData { Cats = _context.Cats.ToList() };
                cats.Filter(Animal, Id, Breed, Age, IsAdopted);



                return Ok(new { error = false, cats.Cats });

            }
            else if (Animal.ToLower() == "dog")
            {
                AnimalData dogs = new AnimalData { Dogs = _context.Dogs.ToList() };
                dogs.Filter(Animal, Id, Breed, Age, IsAdopted);

                return Ok(new { error = false, dogs.Dogs });
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
                Cat newCat = new Cat
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
                Dog newDog = new Dog
                {
                    Name = name,
                    Breed = breed,
                    Age = age,
                    IsAdopted = isAdopted
                };
                _context.Dogs.Add(newDog);
                _context.SaveChanges();

                return Ok(new { error = false, message = "Dog created successfully." });
            }
            else
            {
                return BadRequest(new { error = true, message = "Invalid Animal Type" });
            }
        }


        private dynamic GetAnimalById(int id, string animalType)
        {
            if (animalType.ToLower() == "cat")
            {
                return _context.Cats.FirstOrDefault(cat => cat.Id == id);
            }
            else if (animalType.ToLower() == "dog")
            {
                return _context.Dogs.FirstOrDefault(dog => dog.Id == id);
            }
            else
            {
                return null;
            }
        }


    }
}
