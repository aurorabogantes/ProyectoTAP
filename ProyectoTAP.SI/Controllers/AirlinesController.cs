using Microsoft.AspNetCore.Mvc;

namespace ProyectoTAP.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirlinesController : ControllerBase
    {
        
        private static readonly List<Airline> airlines = new List<Airline>
        {
            new Airline { Id = 1, Name = "Avianca", Phone = "2222-1111" },
            new Airline { Id = 2, Name = "Delta", Phone = "2222-2222" },
            new Airline { Id = 3, Name = "United", Phone = "2222-3333" }
        };

        
        [HttpGet]
        public IActionResult GetAllAirlines()
        {
            return Ok(airlines);
        }

        [HttpGet("{id}")]
        public IActionResult GetAirlineById(int id)
        {
            var airline = airlines.FirstOrDefault(a => a.Id == id);

            if (airline == null)
            {
                return NotFound("Airline not found.");
            }

            return Ok(airline);
        }


        [HttpPost]
        public IActionResult AddAirline([FromBody] Airline airline)
        {
            if (airline == null)
            {
                return BadRequest("Airline data is required.");
            }

            if (string.IsNullOrWhiteSpace(airline.Name))
            {
                return BadRequest("Airline name is required.");
            }

            if (string.IsNullOrWhiteSpace(airline.Phone))
            {
                return BadRequest("Airline phone is required.");
            }

            bool exists = airlines.Any(a =>
                a.Name.Equals(airline.Name, StringComparison.OrdinalIgnoreCase) ||
                a.Phone.Equals(airline.Phone, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return BadRequest("An airline with the same name or phone already exists.");
            }

            airline.Id = airlines.Any() ? airlines.Max(a => a.Id) + 1 : 1;
            airlines.Add(airline);

            return Ok(new
            {
                message = "Airline added successfully.",
                data = airline
            });
        }

        
        [HttpGet("search")]
        public IActionResult SearchAirline([FromQuery] string? name, [FromQuery] string? phone)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(phone))
            {
                return BadRequest("You must provide name or phone.");
            }

            var result = airlines.Where(a =>
                (!string.IsNullOrWhiteSpace(name) && a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrWhiteSpace(phone) && a.Phone.Contains(phone, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            if (!result.Any())
            {
                return NotFound("No airline found with the provided data.");
            }

            return Ok(result);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAirline(int id, [FromBody] Airline updatedAirline)
        {
            if (updatedAirline == null)
            {
                return BadRequest("Airline data is required.");
            }

            if (string.IsNullOrWhiteSpace(updatedAirline.Name))
            {
                return BadRequest("Airline name is required.");
            }

            if (string.IsNullOrWhiteSpace(updatedAirline.Phone))
            {
                return BadRequest("Airline phone is required.");
            }

            var existingAirline = airlines.FirstOrDefault(a => a.Id == id);

            if (existingAirline == null)
            {
                return NotFound("Airline not found.");
            }

            bool duplicateExists = airlines.Any(a =>
                a.Id != id &&
                (
                    a.Name.Equals(updatedAirline.Name, StringComparison.OrdinalIgnoreCase) ||
                    a.Phone.Equals(updatedAirline.Phone, StringComparison.OrdinalIgnoreCase)
                )
            );

            if (duplicateExists)
            {
                return BadRequest("Another airline with the same name or phone already exists.");
            }

            existingAirline.Name = updatedAirline.Name;
            existingAirline.Phone = updatedAirline.Phone;

            return Ok(new
            {
                message = "Airline updated successfully.",
                data = existingAirline
            });
        }
    }


    public class Airline
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}