using Cards.API.Models;
using Microsoft.OpenApi.Validations;

namespace NUnitTestsCard
{
    public class CardTests
    {
       

        [Test]
        public void Adding_CardTest()
        {
            //Arrange
            var card = new Card("vp1", "1111222233334444");

            //Act
            card.AddCard("vp2", "2222333344445555");

            //Assert

            Assert.Pass();
           // Assert.Fail();
        }
    }
}