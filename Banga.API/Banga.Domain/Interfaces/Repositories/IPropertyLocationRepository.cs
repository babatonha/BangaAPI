using Banga.Data.Models;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyLocationRepository
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<string>> GetCitySuburbs();

        Task<IEnumerable<Suburb>> GetSuburbs();
    }
}
