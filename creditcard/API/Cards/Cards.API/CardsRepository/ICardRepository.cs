using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.CardsRepository
{
    public interface ICardRepository
    {
         Task<List<Card>> GetCards();

    }
}
