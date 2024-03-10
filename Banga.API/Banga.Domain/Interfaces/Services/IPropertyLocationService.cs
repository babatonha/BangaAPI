using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyLocationService
    {
        Task<IEnumerable<City>> GetCities();
    }
}
