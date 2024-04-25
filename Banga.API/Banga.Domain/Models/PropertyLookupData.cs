using Banga.Data.Models;

namespace Banga.Domain.Models
{
    public class PropertyLookupData
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Suburb> Suburbs { get; set; }
        public IEnumerable<LawFirm> LawFirms { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public IEnumerable<RegistrationType> RegistrationTypes { get; set; }

    }
}
