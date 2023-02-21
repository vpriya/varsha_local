using Cards.API.Data;
using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.CardsRepository
{
    public class sqlCardRepository: ICardRepository
    {
        // this is used as another layer for data logic/manipulation, controller not needed for logic
        private readonly CardsDbContext _cardsDbContext;
        public sqlCardRepository(CardsDbContext cardsDbContext)
        {
            this._cardsDbContext = cardsDbContext;
        }

        public async Task<List<Card>> GetCards()
        {
            var cards = await _cardsDbContext.Cards.ToListAsync();
            return cards;
        }

        public async Task<Card?> GetOneCard(Guid id)
        {
            var oneCard = await _cardsDbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            return oneCard;
            
        }

        public async Task<Card?> AddOneCard(Card addRequestedCard)
        {
            addRequestedCard.Id = Guid.NewGuid();
            var result = await _cardsDbContext.Cards.AddAsync(addRequestedCard);
            await _cardsDbContext.SaveChangesAsync();
            //return addRequestedCard;
            return result.Entity;
            //throw new NotImplementedException();
        }

        public async Task DeleteOneCard(Guid id)
        {
            //throw new NotImplementedException();
            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                _cardsDbContext.Cards.Remove(existingCard);
                await _cardsDbContext.SaveChangesAsync();
            }

        }

        public async Task<Card?> UpdateOneCard(Guid id, Card cardToUpdate)
        {
            //throw new NotImplementedException();
            var existingCard = await _cardsDbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            
            if (existingCard != null)
            {
                existingCard.CardholderName = cardToUpdate.CardholderName;
                existingCard.CardNumber = cardToUpdate.CardNumber;
                existingCard.ExpiryMonth = cardToUpdate.ExpiryMonth;
                existingCard.ExpiryYear = cardToUpdate.ExpiryYear;
                existingCard.CVC = cardToUpdate.CVC;
                await _cardsDbContext.SaveChangesAsync();
                return existingCard;
            }

            return null;

        }
    }
}
