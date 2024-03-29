﻿using Cards.API.DTOdomainModel;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.CardsRepository
{
    public interface ICardRepository
    {
        //These methods will be defined in SqlRepository method and will be called inside the controller
        Task<List<CardDto>> GetCards();
        Task<CardDto> GetOneCard(Guid id);
        Task<CardDto> AddOneCard(CardDto card);
        Task <Boolean> DeleteOneCard(Guid id);
        Task<CardDto?> UpdateOneCard(Guid id, CardDto card);
    }
}
