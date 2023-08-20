#nullable disable

using Microsoft.AspNetCore.Mvc;
using AnimalShelterAPI.Models;
using System.Reflection.Metadata;

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

        /// <summary>
        /// Retrieves a list of animals from the animal shelter.
        /// </summary>
        /// <param name="Animal">Filter by animal type ("both", "cat", "dog")</param>
        /// <param name="Id">Filter by animal ID</param>
        /// <param name="Breed">Filter by animal breed</param>
        /// <param name="Age">Filter by animal age</param>
        /// <param name="IsAdopted">Filter by adoption status</param>
        /// <returns>A list of animals based on the provided filters.</returns>
        [HttpGet]
        public IActionResult Get(string? Animal, int? Id, string? Breed, int? Age, bool IsAdopted)
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

        /// <summary>
        /// Creates a new animal in the animal shelter.
        /// </summary>
        /// <param name="animal">Animal type ("cat" or "dog")</param>
        /// <param name="name">Animal name</param>
        /// <param name="breed">Animal breed</param>
        /// <param name="age">Animal age</param>
        /// <returns>Response indicating the success of the operation.</returns>
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

        /// <summary>
        /// Updates an existing animal in the animal shelter.
        /// </summary>
        /// <param name="id">Animal ID</param>
        /// <param name="animal">Animal type ("cat" or "dog")</param>
        /// <param name="name">Updated animal name</param>
        /// <param name="breed">Updated animal breed</param>
        /// <param name="age">Updated animal age</param>
        /// <param name="isAdopted">Updated adoption status</param>
        /// <returns>Response indicating the success of the operation.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, string animal, string name, string breed, int age, bool isAdopted)
        {
            if (string.IsNullOrEmpty(animal))
            {
                return BadRequest(new { error = true, message = "Invalid input data." });
            }
            Console.WriteLine(id);
            dynamic animalToUpdate = GetAnimalById(id, animal);

            if (animalToUpdate == null)
            {
                return NotFound(new { error = true, message = "Animal not found." });
            }

            animalToUpdate.Name = name;
            animalToUpdate.Breed = breed;
            animalToUpdate.Age = age;
            animalToUpdate.IsAdopted = isAdopted;

            _context.SaveChanges();

            return Ok(new { error = false, message = "Animal updated successfully." });
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

        /// <summary>
        /// Deletes an existing animal from the animal shelter.
        /// </summary>
        /// <param name="id">Animal ID</param>
        /// <param name="animal">Animal type ("cat" or "dog")</param>
        /// <returns>Response indicating the success of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, string animal)
        {
            dynamic animalToDelete = GetAnimalById(id, animal);

            if (animalToDelete == null)
            {
                return NotFound(new { error = true, message = "Animal not found." });
            }

            if (animal.ToLower() == "cat")
            {
                _context.Cats.Remove(animalToDelete);
            }
            else if (animal.ToLower() == "dog")
            {
                _context.Dogs.Remove(animalToDelete);
            }
            else
            {
                return BadRequest(new { error = true, message = "Invalid Animal Type" });
            }

            _context.SaveChanges();

            return Ok(new { error = false, message = "Animal deleted successfully.", Documentation= "/api/docs"});
        }


    }
}
