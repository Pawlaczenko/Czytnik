using Microsoft.EntityFrameworkCore;

namespace Czytnik.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
        : base(options)
        {
        }
    }
}
