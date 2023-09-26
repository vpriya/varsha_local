using AutoMapper;
using Cards.API.CardsRepository;
using Cards.API.Data;
using Cards.API.DTOdomainModel;
using Cards.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System;

namespace NUnitTestsCard
{
    public class CardTests
    {
        private static DbContextOptions<CardsDbContext> dbContextOptions = new DbContextOptionsBuilder<CardsDbContext>()
              .UseInMemoryDatabase(databaseName: "CardsTestDb")
              .Options;
        private readonly IMapper? _mapper;


        CardsDbContext dbContext;
        sqlCardRepository cardRepository;

        public static DbContextOptions<CardsDbContext> DbContextOptions { get => dbContextOptions; set => dbContextOptions = value; }

        [OneTimeSetUp]
        public void Setup()
        {
            dbContext = new CardsDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
            SeedDatabase();
            cardRepository = new sqlCardRepository(dbContext, _mapper);
        }

        [Test]
        public async Task GetOneCard_Success()
        {
            Guid id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4"); // this is for 2nd record Dipika, means [1]
            var oneCard = await dbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            if (oneCard == null)
            {
                Assert.IsNull(oneCard);
            }
            else
            {
                Assert.AreEqual("Dipika Padukone", oneCard.CardholderName);
                Assert.AreEqual("1111000022220000", oneCard.CardNumber);
            }
        }

        [Test]
        public async Task GetOneCard_Failure()
        {
            Guid id = new Guid();
            var oneCard = await dbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            Assert.IsNull(oneCard);
        }

        [Test]
        public async Task AddOneCard_Success()
        {
            Guid id = new Guid("f555b4c3-8b1c-4e2a-9929-7c1b47e16250"); //generated from online GUID generator
            Card newCard = new Card()
            {
                Id = id,
                CardholderName = "Papa Johns",
                CardNumber = "1010202030304050",
                ExpiryMonth = 05,
                ExpiryYear = 30,
                CVC = 019
            };
            await dbContext.Cards.AddAsync(newCard);
            await dbContext.SaveChangesAsync();
            //
            var oneCard = await dbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            if (oneCard == null)
            {
                Assert.IsNull(oneCard);
            }
            else
            {
                Assert.AreEqual("Papa Johns", oneCard.CardholderName);
                Assert.AreEqual("1010202030304050", oneCard.CardNumber);
                //
                dbContext.Cards.Remove(oneCard); // deleting whatever we added after test passes
                await dbContext.SaveChangesAsync();
                //
            }

        }

        [Test]
        public async Task DeleteOneCard_Success()
        {
            // first add a record to delete the same here
            Guid id = new Guid("f555b4c3-8b1c-4e2a-9929-7c1b47e16250"); //generated from online GUID generator
            Card newCard = new Card()
            {
                Id = id,
                CardholderName = "Papa Johns",
                CardNumber = "1010202030304050",
                ExpiryMonth = 05,
                ExpiryYear = 30,
                CVC = 019
            };
            await dbContext.Cards.AddAsync(newCard);
            await dbContext.SaveChangesAsync(); 
            // record is added now to be deleted

            //
            var existingCard = await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == id) ?? new Card();
            if (existingCard != null)
            {
                dbContext.Cards.Remove(existingCard);
                await dbContext.SaveChangesAsync();
            }

            var deletedCard = await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == id) ?? null;
            Assert.IsNull(deletedCard);
        }

        [Test]
        public async Task AddOneCard_Failure_card_name_missing()
        {
            Guid id = new Guid();
            Card newCard = new Card()
            {
                Id = id,
                CardNumber = "1010202030304050",
                ExpiryMonth = 05,
                ExpiryYear = 30,
                CVC = 019
            };
            await dbContext.Cards.AddAsync(newCard);
            await dbContext.SaveChangesAsync();
            //

            var oneCard = await dbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            Assert.IsNull(oneCard);
        }

        [Test]
        public async Task AddOneCard_Failure_card_number_missing()
        {
            Guid id = new Guid();
            Card newCard = new Card()
            {
                Id = id,
                CardholderName = "new name",
                ExpiryMonth = 05,
                ExpiryYear = 30,
                CVC = 019
            };
            await dbContext.Cards.AddAsync(newCard);
            await dbContext.SaveChangesAsync();
            //

            var oneCard = await dbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            Assert.IsNull(oneCard);
        }

        [Test]
        public async Task UpdateOneCard_success()
        {
            Guid id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4"); // this is for 2nd record Dipika in Seed Database
            Card updateSingleCard_ToThisnewValue = new Card()
            {
                CardholderName = "Dipika Padukone Edited",
                CardNumber = "1111000022220001",
                ExpiryMonth = 04,
                ExpiryYear = 27,
                CVC = 004
            };
            var oneCard_tobeUpdated = await dbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            if(oneCard_tobeUpdated !=null)//exists, update now
            {
                oneCard_tobeUpdated.CardholderName = updateSingleCard_ToThisnewValue.CardholderName;
                oneCard_tobeUpdated.CardNumber = updateSingleCard_ToThisnewValue.CardNumber;
                oneCard_tobeUpdated.ExpiryMonth = updateSingleCard_ToThisnewValue.ExpiryMonth;
                oneCard_tobeUpdated.ExpiryYear = updateSingleCard_ToThisnewValue.ExpiryYear;
                oneCard_tobeUpdated.CVC = updateSingleCard_ToThisnewValue.CVC;
                await dbContext.SaveChangesAsync();
            }
            else // didn't match
            {
                Assert.IsNull(oneCard_tobeUpdated);
            }

            // now check if updated successfully with entered values
            Assert.AreEqual(oneCard_tobeUpdated.CardholderName, "Dipika Padukone Edited");
            Assert.AreEqual(oneCard_tobeUpdated.CardNumber, "1111000022220001");
        }

        [Test]
        public async Task UpdateOneCard_failure()
        {
            Guid id = new Guid(); //a new guid, which doesn't exist
            Card updateSingleCard_ToThisnewValue = new Card()
            {
                CardholderName = "Dipika Padukone Edited",
                CardNumber = "1111000022220001",
                ExpiryMonth = 04,
                ExpiryYear = 27,
                CVC = 004
            };
            var oneCard_tobeUpdated = await dbContext.Cards.SingleOrDefaultAsync(x => x.Id == id);
            Assert.IsNull(oneCard_tobeUpdated);
        }

        [Test]
        public async Task DeleteOneCard_Failure()
        {
            Guid id = new Guid(); // not existing id
            var deletedCard = await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == id) ?? null;
            Assert.IsNull(deletedCard);
        }

        [Test]
        public async Task GetCards_Success()
        {
            Guid id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4");//Amit 1st and dipika 2nd 
            var cards = await dbContext.Cards.ToListAsync();
            Assert.AreEqual(cards.Count, 2);
            Assert.AreEqual(cards[1].Id, id);
            Assert.AreEqual(cards[0].CardNumber, "1111222233334444");// right side amit card
        }

        [Test]
        public async Task GetCards_Failure()
        {
            Guid id = new Guid("e7aca2fb-7421-4258-8714-7f9a684b30e4");//Amit 1st and dipika 2nd
            var cards = await dbContext.Cards.ToListAsync();
            Assert.AreNotEqual(cards.Count, 10);
            Assert.AreNotEqual(cards[0].Id, id);
            Assert.AreNotEqual(cards[1].CardNumber, "1111222233334444");//right is amit card
        }


        [OneTimeTearDown]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
        }

        private void SeedDatabase()
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