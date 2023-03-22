using Cards.API.DTOdomainModel;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }
        // Dbset
        public DbSet<CardDto> Cards { get; set; }

    }
}
