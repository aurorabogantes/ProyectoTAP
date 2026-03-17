using Microsoft.AspNetCore.Mvc;

namespace ProyectoTAP.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirlinesController : ControllerBase
    {
        [HttpGet("search")]
        public IActionResult SearchAirline([FromQuery] string? name, [FromQuery] string? phone)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(phone))
            {
                return BadRequest("You must provide name or phone.");
            }

            var airlines = new[]
            {
              new { Id = 1, Name = "Avianca", Phone = "2222-1111" },
             new { Id = 2, Name = "Delta", Phone = "2222-2222" },
             new { Id = 3, Name = "United", Phone = "2222-3333" }
            };

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
    }
}