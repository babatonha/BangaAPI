using Banga.Data.Models;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyLocationService
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<string>> GetCitySuburbs();

        Task<IEnumerable<Suburb>> GetSuburbs();
    }
}
