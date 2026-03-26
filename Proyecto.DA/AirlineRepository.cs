using ProyectoTAP.SI;
using ProyectoTAP.DataAccess;
using ProyectoTAP.Model;
using ProyectoTAP.SI.Controllers;

namespace ProyectoTAP.DataAccess
{
    public class AirlineRepository
    {
        // Lista de aerolíneas en memoria
        private static readonly List<Airline> airlines = new List<Airline>
        {
            new Airline { Id = 1, Name = "Avianca", Phone = "2222-1111" },
            new Airline { Id = 2, Name = "Delta", Phone = "2222-2222" },
            new Airline { Id = 3, Name = "United", Phone = "2222-3333" }
        };

        // Obtener todas las aerolíneas
        public List<Airline> GetAllAirlines()
        {
            return airlines;
        }

        // Obtener una aerolínea por Id
        public Airline GetAirlineById(int id)
        {
            return airlines.FirstOrDefault(a => a.Id == id);
        }

        // Agregar una nueva aerolínea
        public void AddAirline(Airline airline)
        {
            airline.Id = airlines.Any() ? airlines.Max(a => a.Id) + 1 : 1;
            airlines.Add(airline);
        }

        // Actualizar una aerolínea
        public bool UpdateAirline(int id, Airline updatedAirline)
        {
            var existingAirline = GetAirlineById(id);
            if (existingAirline == null)
                return false;

            existingAirline.Name = updatedAirline.Name;
            existingAirline.Phone = updatedAirline.Phone;
            return true;
        }

        // Buscar aerolíneas por nombre o teléfono
        public List<Airline> SearchAirline(string name = null, string phone = null)
        {
            return airlines.Where(a =>
                (string.IsNullOrEmpty(name) || a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(phone) || a.Phone.Contains(phone, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }
}