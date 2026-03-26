using ProyectoTAP.DataAccess;
using ProyectoTAP.Model;

namespace ProyectoTAP.BusinessLogic
{
    public class AirlineService
    {
        private readonly AirlineRepository _airlineRepository;

        public AirlineService()
        {
            _airlineRepository = new AirlineRepository(); // Instancia de acceso a datos
        }

        // Obtener todas las aerolíneas
        public List<Airline> GetAllAirlines()
        {
            return _airlineRepository.GetAllAirlines();
        }

        // Obtener aerolínea por Id
        public Airline GetAirlineById(int id)
        {
            return _airlineRepository.GetAirlineById(id);
        }

        // Agregar una nueva aerolínea
        public string AddAirline(Airline airline)
        {
            var existingAirline = _airlineRepository.GetAllAirlines()
                .Any(a => a.Name.Equals(airline.Name, StringComparison.OrdinalIgnoreCase) || a.Phone.Equals(airline.Phone, StringComparison.OrdinalIgnoreCase));

            if (existingAirline)
            {
                return "An airline with the same name or phone already exists.";
            }

            _airlineRepository.AddAirline(airline);
            return "Airline added successfully.";
        }

        // Actualizar una aerolínea
        public string UpdateAirline(int id, Airline updatedAirline)
        {
            if (!_airlineRepository.UpdateAirline(id, updatedAirline))
            {
                return "Airline not found.";
            }

            return "Airline updated successfully.";
        }

        // Buscar aerolíneas por nombre o teléfono
        public List<Airline> SearchAirline(string name = null, string phone = null)
        {
            return _airlineRepository.SearchAirline(name, phone);
        }
    }
}