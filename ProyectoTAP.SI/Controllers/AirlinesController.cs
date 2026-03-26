using Microsoft.AspNetCore.Mvc;
using ProyectoTAP.BusinessLogic;
using ProyectoTAP.Model;

namespace ProyectoTAP.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirlinesController : ControllerBase
    {
        private readonly AirlineService _airlineService;

        public AirlinesController()
        {
            _airlineService = new AirlineService(); // Instancia del servicio de negocio
        }

        [HttpGet]
        public IActionResult GetAllAirlines()
        {
            var airlines = _airlineService.GetAllAirlines();
            return Ok(airlines);
        }

        [HttpGet("{id}")]
        public IActionResult GetAirlineById(int id)
        {
            var airline = _airlineService.GetAirlineById(id);
            if (airline == null)
            {
                return NotFound("Airline not found.");
            }
            return Ok(airline);
        }

        [HttpPost]
        public IActionResult AddAirline([FromBody] Airline airline)
        {
            var message = _airlineService.AddAirline(airline);
            if (message == "Airline added successfully.")
            {
                return Ok(message);
            }

            return BadRequest(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAirline(int id, [FromBody] Airline updatedAirline)
        {
            var message = _airlineService.UpdateAirline(id, updatedAirline);
            if (message == "Airline updated successfully.")
            {
                return Ok(message);
            }

            return BadRequest(message);
        }

        [HttpGet("search")]
        public IActionResult SearchAirline([FromQuery] string? name, [FromQuery] string? phone)
        {
            var airlines = _airlineService.SearchAirline(name, phone);
            if (!airlines.Any())
            {
                return NotFound("No airline found.");
            }

            return Ok(airlines);
        }
    }
}