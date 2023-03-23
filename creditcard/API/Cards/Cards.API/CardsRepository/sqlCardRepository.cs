using AutoMapper;
using Cards.API.Data;
using Cards.API.DTOdomainModel;
using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.CardsRepository
{
    public class sqlCardRepository : ICardRepository
    {
        // this is used as another layer for data logic/manipulation, controller not needed for logic
        private readonly CardsDbContext _cardsDbContext;
        private readonly IMapper _mapper;
        public sqlCardRepository(CardsDbContext cardsDbContext, IMapper mapper)
        {
            this._cardsDbContext = cardsDbContext;
            _mapper = mapper;
        }

        public async Task<List<CardDto>> GetCards()
        {
            var cards = await _cardsDbContext.Cards.ToListAsync();
            var allCardsDto = _mapper.Map<List<CardDto>>(cards); // destination is CardDto and source is cards (list of cards)
            return allCardsDto;
        }

        public async Task<CardDto> GetOneCard(Guid id)
        {
            var oneCard = await _cardsDbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);// use dbcontect here first
            var oneCardDto = _mapper.Map<CardDto>(oneCard); // destination is CardDto and source is oneCard
            return oneCardDto;
        }

        public async Task<CardDto> AddOneCard(CardDto addRequestedCard)
        {
            addRequestedCard.Id = Guid.NewGuid();
            var card_toAdd = _mapper.Map<Card>(addRequestedCard); // now the Card is the destination and CardDto is the source
            await _cardsDbContext.Cards.AddAsync(card_toAdd);
            await _cardsDbContext.SaveChangesAsync();
            return addRequestedCard; 
        }

        public async Task <Boolean> DeleteOneCard(Guid id)
        {
            //throw new NotImplementedException();
            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                _cardsDbContext.Cards.Remove(existingCard);
                await _cardsDbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<CardDto?> UpdateOneCard(Guid id, CardDto cardToUpdate)
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
                return cardToUpdate;
            }
            return null;
        }
    }
}
