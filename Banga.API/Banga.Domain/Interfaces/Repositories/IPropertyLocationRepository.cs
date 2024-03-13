using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyLocationRepository
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<string>> GetCitySuburbs(); 
    }
}
