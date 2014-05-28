using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.Services;
using Moq;
using NUnit.Framework;

namespace DrynksMes.Services.Tests.Services
{
    [TestFixture]
    public class DrinksServicesTests
    {
       
        private DrinksServices _drinksServices;
        private Mock<IDatabaseContext> _dbContext;


        [SetUp]
        public void DrynksServicesSetup()
        {
            _dbContext = new Mock<IDatabaseContext>();


            _drinksServices = new DrinksServices(_dbContext.Object);
        }

        [Test]
        public void GetDrinksForUser_OnPassingUserId_ReturnsAllTheDrinksAssociated()
        {
            //_mockdrinksRepository.Setup(
            //    m => m.GetMany(It.IsAny<Expression<Func<Drink, bool>>>())).Returns(
            //    new[]
            //        {
            //           new Drink{DrinkName= "Fake1",DrinkDescription="FakeDesr",DrinkId= 1},
            //           new Drink{DrinkName= "Fake2",DrinkDescription="FakeDescr2",DrinkId= 2}
            //        });
            //var drinks = _drinksServices.GetDrinksForUser(It.IsAny<int>()).ToList();
            //Assert.IsNotNull(drinks);
            //Assert.AreEqual(2,drinks.Count());

        }

        [Test]
        public void GetAllDrinks_OnGettingCalled_ReturnsAllThreDrinks()
        {
           //_mockdrinksRepository.Setup(
           //     m => m.GetAll()).Returns(
           //     new[]
           //         {
           //            new Drink{DrinkName= "Fake1",DrinkDescription="FakeDesr",DrinkId= 1},
           //            new Drink{DrinkName= "Fake2",DrinkDescription="FakeDescr2",DrinkId= 2}
           //         });
           // var drinks = _drinksServices.GetAllDrinks().ToList();
           // Assert.IsNotNull(drinks);
           // Assert.AreEqual(2, drinks.Count());

        }
    }
}
