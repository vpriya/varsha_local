using AutoMapper;
using Cards.API.CardsRepository;
using Cards.API.Data;
using Cards.API.DTOdomainModel;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly CardsDbContext _cardsDbContext;
        private ICardRepository _iCardRepositoryData;
        private readonly IMapper _mapper; //automapper provides IMapper interface
        
        public CardsController(CardsDbContext cardsDbContext, ICardRepository iCardRepo, IMapper mapper)
        {
            _cardsDbContext = cardsDbContext;
            _iCardRepositoryData = iCardRepo;
            _mapper = mapper;
        }

        // Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _iCardRepositoryData.GetCards();
            var cardsDto = _mapper.Map<List<CardDto>>(cards); // destination is CardDto and source is cards
            return Ok(cardsDto);
        }

        // Get single Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var searchcard = await _iCardRepositoryData.GetOneCard(id);
            if (searchcard != null)
            {
                return Ok(_mapper.Map<CardDto>(searchcard));
            }
            else
            {
                return NotFound("Card not found");
            }
        }

        // Add a Card - Post the data is JSON format
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardDto cardToAddDto)
        {
            var newlyAddedCard = await _iCardRepositoryData.AddOneCard(cardToAddDto);
            if (newlyAddedCard != null)
            {
                return Ok(cardToAddDto);
            }
            return BadRequest();
        }

        // Updating A Card
        // PUT: api/Cards/id
        [HttpPut]
        [Route("{id:guid}")]
        public async Task <IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] CardDto cardToUpdateDTo)
        {
           /*var ChangedCard = await _iCardRepositoryData.UpdateOneCard(id, cardToUpdateDTo);
            if (ChangedCard != null)
            {
                return  Ok(cardToUpdateDTo);
            }
            return NotFound("Card not found");*/


            try
            {
                await _iCardRepositoryData.UpdateOneCard(id, cardToUpdateDTo);
            }
            catch (DbUpdateConcurrencyException)
            {
                var ChangedCard2 = await _iCardRepositoryData.UpdateOneCard(id, cardToUpdateDTo);
                if (ChangedCard2 == null)
                {
                    return NotFound("Card not found");
                }
                else
                {
                    throw;
                }

            }
            return Ok(cardToUpdateDTo);
            //return NoContent();



        }

        // Delete A Card
        // DELETE: api/Cards/id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var ifDeleted= await _iCardRepositoryData.DeleteOneCard(id);
            if (ifDeleted)
            {
                 return NoContent();
            }
            return NotFound("Card not found");
        }

    }
}
