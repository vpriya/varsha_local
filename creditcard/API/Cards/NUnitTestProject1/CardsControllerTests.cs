using AutoMapper;
using Cards.API.CardsRepository;
using Cards.API.Data;
using Cards.API.DTOdomainModel;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestsCard
{
    internal class CardsControllerTests
    {
        private readonly CardsDbContext _cardsDbContext;
        private ICardRepository _iCardRepositoryData;
        private readonly IMapper _mapper; //automapper provides IMapper interface

        CardsDbContext dbContext;

        public CardsControllerTests(CardsDbContext cardsDbContext, ICardRepository iCardRepo, IMapper mapper)
        {
            _cardsDbContext = cardsDbContext;
            _iCardRepositoryData = iCardRepo;
            _mapper = mapper;
        }

        private static DbContextOptions<CardsDbContext> dbContextOptions = new DbContextOptionsBuilder<CardsDbContext>()
             .UseInMemoryDatabase(databaseName: "CardsTestDb")
             .Options;

        public static DbContextOptions<CardsDbContext> DbContextOptions { get => dbContextOptions; set => dbContextOptions = value; }

        [OneTimeSetUp]
        public void Setup()
        {
            dbContext = new CardsDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
            SeedDatabase2();
        }

        // Get All Cards
        [Test]
        public async Task GetAllCards_Success()
        {
            var cards = await _iCardRepositoryData.GetCards();
            var cardsDto = _mapper.Map<List<CardDto>>(cards); // destination is CardDto and source is cards
            Guid id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4");
            Assert.AreEqual(cards.Count, 2);
            Assert.AreEqual(cards[1].Id, id);
            Assert.AreEqual(cards[0].CardNumber, "1111222233334444");
        }
       
        //public async Task GetCards_Success()
        //{
        //    Guid id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4");//Amit 1st and dipika 2nd 
        //    var cards = await dbContext.Cards.ToListAsync();
        //    Assert.AreEqual(cards.Count, 2);
        //    Assert.AreEqual(cards[1].Id, id);
        //    Assert.AreEqual(cards[0].CardNumber, "1111222233334444");// right side amit card
        //}

      
       /* public async Task GetCards_Failure()
        {
            Guid id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4");//Amit 1st and dipika 2nd
            var cards = await dbContext.Cards.ToListAsync();
            Assert.AreNotEqual(cards.Count, 10);
            Assert.AreNotEqual(cards[0].Id, id);
            Assert.AreNotEqual(cards[1].CardNumber, "1111222233334444");//right is amit card
        }
       */










        [OneTimeTearDown]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
        }

        private void SeedDatabase2()
        {
            var card = new List<Card>()
            {
                new Card()
                {
                    Id = new Guid("d4e05990-d581-4399-824e-c2241fda4b6f"),
                    CardholderName = "Amit Kumar",
                    CardNumber = "1111222233334444",
                    ExpiryMonth = 03,
                    ExpiryYear = 26,
                    CVC = 003
                },
                new Card()
                {
                    Id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4"),
                    CardholderName = "Dipika Padukone",
                    CardNumber = "1111000022220000",
                    ExpiryMonth = 04,
                    ExpiryYear = 27,
                    CVC = 004
                }

            };
            dbContext.Cards.AddRange(card);
            dbContext.SaveChanges();

        }


    }
}
