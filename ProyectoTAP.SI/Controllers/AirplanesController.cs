using Microsoft.AspNetCore.Mvc;

namespace ProyectoTAP.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirplanesController : ControllerBase
    {
        [HttpGet("by-airline")]
        public IActionResult GetAirplanesByAirline([FromQuery] string? airlineName)
        {
            if (string.IsNullOrWhiteSpace(airlineName))
            {
                return BadRequest("You must provide the airline name.");
            }

            var airplanes = new[]
            {
                new { Id = 1, Name = "Boeing 737", Model = "737-800", AirlineName = "Avianca" },
                new { Id = 2, Name = "Airbus A320", Model = "A320", AirlineName = "Avianca" },
                new { Id = 3, Name = "Boeing 777", Model = "777-300ER", AirlineName = "Delta" },
                new { Id = 4, Name = "Airbus A319", Model = "A319", AirlineName = "United" }
            };

            var result = airplanes
                .Where(a => a.AirlineName.Contains(airlineName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!result.Any())
            {
                return NotFound("No airplanes found for the provided airline.");
            }

            return Ok(result);
        }
    }
}