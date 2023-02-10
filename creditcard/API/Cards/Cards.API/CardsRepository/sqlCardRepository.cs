using Cards.API.Data;
using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.CardsRepository
{
    public class sqlCardRepository: ICardRepository
    {
        // this is used as another layer for data logic/manipulation, controller not needed for logic
        private readonly CardsDbContext cardsDbContext;
        public sqlCardRepository(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext = cardsDbContext;
        }

        public async Task<List<Card>> GetCards()
        {
            var cards = await cardsDbContext.Cards.ToListAsync();
            return cards;
        }







    }
}
