using Banga.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Banga.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users {get; set;}
    }
}
