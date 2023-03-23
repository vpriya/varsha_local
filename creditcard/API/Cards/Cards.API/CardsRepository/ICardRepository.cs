using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.CardsRepository
{
    public interface ICardRepository
    {
        //These methods will be defined in SqlRepository method and will be called inside the controller
        Task<List<Card>> GetCards();
        Task<Card?> GetOneCard(Guid id);
        Task<Card?> AddOneCard(Card card);
        Task DeleteOneCard(Guid id);
        Task<Card?> UpdateOneCard(Guid id, Card card);

    }
}
