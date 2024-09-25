using Banga.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Banga.Domain.Models
{
    public class PaginatedList
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<Property> Items { get; set; }

    }
}
