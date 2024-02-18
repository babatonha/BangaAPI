using Banga.Data.Models;

namespace Banga.Data.ViewModels
{
    public class VwProperty
    {
        public Property? Property { get; set; }
        public IEnumerable<PropertyOffer>? PropertyOffers { get; set; }
        public IEnumerable<PropertyPhoto>? PropertyPhotos { get; set; }
    }
}
