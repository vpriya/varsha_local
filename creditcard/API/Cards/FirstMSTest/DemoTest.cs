using Cards.API.CardsRepository;
using Cards.API.DTOdomainModel;
using Cards.API.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMSTestProject
{
    [TestClass]
    public  class DemoTest
    {
        [TestMethod]
        public void TestGetCardBy_Id_Method(Guid guid)
        {
            var card = new Card
            {
               Id = new Guid ("d8ee618d-2955-419b-accc-a31195ebe884"),
                CardholderName="Amit Kumar",
                CardNumber="1111222233334444",
                ExpiryMonth=03,
                ExpiryYear=26,
                CVC=003
            };
            var cardRepository = new Mock<ICardRepository>();
            cardRepository.Setup(c => c.GetCardById(It.IsAny<int>()))guid.Id = card.Id;)).Returns(card);

            //var searchcard = await _iCardRepositoryData.GetOneCard(id);
            //if (searchcard != null)
            //{
            //    return Ok(_mapper.Map<CardDto>(searchcard));
            //}
            //else
            //{
            //    return NotFound("Card not found");
            //}

        }
        //----------------------------------------------------------------------

        [TestMethod]
        public void TestGetSum()
        {
            int x=9, y=10;
            var result=GetSum(x, y);
            Assert.AreEqual(x + y,result);
            Assert.AreNotEqual(x-y,result);
        }

        [TestMethod]
        public void TestGetFullName()
        {
            string firstName = "Tailor";
            string lastName = "Swift";
            var result=GetFullName(firstName, lastName);    
            Assert.AreEqual(firstName + lastName, result);
            Assert.IsNotNull(result);
        }

        private int GetSum(int x, int y)
        {
            return x + y;
        }

        private string GetFullName(string firstName, string lastName)       
        { 
            string fullName = firstName + lastName;
            return fullName;

        }
        //-------------------------------------------------------

        private List<Card> SeedDatabase()
        {

        }










        //

    }
}
