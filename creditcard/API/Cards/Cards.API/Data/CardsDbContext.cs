using Cards.API.DTOdomainModel;
using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions<CardsDbContext> options) : base(options)
        {
        }
        // Dbset
        public DbSet<Card> Cards { get; set; }

    }
}
