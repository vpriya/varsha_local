using Cards.API.CardsRepository;
using Cards.API.Data;
using Cards.API.DTO_domainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Cards.API.Models;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly CardsDbContext _cardsDbContext;
        private ICardRepository iCardRepositoryData;
        //Inject AutoMapper here in controller
        private readonly IMapper _mapper;

        public CardsController(CardsDbContext cardsDbContext, ICardRepository iCardRepo, IMapper mapper)
        {
            _cardsDbContext = cardsDbContext;
            iCardRepositoryData = iCardRepo;
            _mapper = mapper;
        }

        // Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            // var cards = await cardsDbContext.Cards.ToListAsync();
            var cards = await iCardRepositoryData.GetCards();
            var mappedCards = _mapper.Map<List<CardDto>>(cards);
            return Ok(mappedCards);
        }

        // Get single Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await iCardRepositoryData.GetOneCard(id);
            if (card != null)
            {
                return Ok(card);
            }
            else
            {
                return NotFound("Card not found");
            }
        }

        // Add a Card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardDto cardRequest)
        {
            var card = _mapper.Map<Card>(cardRequest);
            var newlyAddedCard = await iCardRepositoryData.AddOneCard(card); 
            if (newlyAddedCard != null)
            {
                return Ok(newlyAddedCard);
            }
            return BadRequest(); 
        }

        // Updating A Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task <IActionResult> UpdateCard([FromRoute] Guid id, CardDto updateCardRequest)
        {
            var card = _mapper.Map<Card>(updateCardRequest);
            var existingCard = await iCardRepositoryData.UpdateOneCard(id, card);
            if(existingCard != null)
            {
                return Ok(updateCardRequest);
            }
            return BadRequest();
        }

        // Delete A Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await iCardRepositoryData.DeleteOneCard(id);

            if (existingCard == null)
            {
                return NotFound();
            }
            return Ok();



        }




    }
}
