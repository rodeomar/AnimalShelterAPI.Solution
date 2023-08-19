using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AnimalShelterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
    }
}
