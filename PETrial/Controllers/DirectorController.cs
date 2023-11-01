using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PETrial.Models;

namespace PETrial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : Controller
    {
        private readonly PE_PRN_Fall22B1Context _context;
        public DirectorController(PE_PRN_Fall22B1Context context) 
        {
            _context = context;
        }

        [HttpGet("GetDirectors/{nationality}/{gender}")]
        public IActionResult GetDirectors(string nationality, string gender)
        {
            var result = _context.Directors.Where(x=>x.Nationality.ToLower() == nationality.ToLower()
            && x.Male == (gender.ToLower() == "male")).ToList();
            if (result == null)
            {
                return NotFound();
            }
            var result1 = result.Select(x => new DirectorDTO()
            {
                Id = x.Id,
                FullName = x.FullName,
                Description = x.Description,
                Dob = x.Dob,
                DobString = x.Dob.ToString("MM/dd/yyyy"),
                Nationality = x.Nationality,
                Gender = x.Male == true ? "Male" : "Female"
            });
            return Ok(result1);
        }

        [HttpGet("GetDirector/{id}")]
        public IActionResult GetDirector(int id)
        {
            var res = _context.Directors.Include(x => x.Movies).ThenInclude(x => x.Producer).FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            var result = new DirectorDTOwithMovies
            {
                Id = res.Id,
                FullName = res.FullName,
                Description = res.Description,
                Dob = res.Dob,
                DobString = res.Dob.ToString("MM/dd/yyyy"),
                Nationality = res.Nationality,
                Gender = res.Male == true ? "Male" : "Female",
                Movies = res.Movies.Select(x => new MovieDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    DirectorId = x.DirectorId,
                    DirectorName = x.Director?.FullName,
                    ProducerId = x.ProducerId,
                    ProducerName = x.Producer?.Name,
                    Language = x.Language,
                    ReleaseDate = x.ReleaseDate,
                    ReleaseYear = x.ReleaseDate?.Year.ToString(),
                    Title = x.Title,
                    Genres = x.Genres,
                    Stars = x.Stars,
                }).ToList()
            };
            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create(DirectorDTORequest requestDirector)
        {
            try
            {
                var director = new Director()
                {
                    Description = requestDirector.Description,
                    FullName = requestDirector.FullName,
                    Dob = Convert.ToDateTime(requestDirector.Dob),
                    Nationality = requestDirector.Nationality,
                    Male = requestDirector.Male
                };
                _context.Directors.Add(director);
                var row_inserted = _context.SaveChanges();
                return Ok(row_inserted);
            }
            catch (Exception ex) 
            { 
                return Conflict("There is an error while adding"); 
            }
        }
    }
}
