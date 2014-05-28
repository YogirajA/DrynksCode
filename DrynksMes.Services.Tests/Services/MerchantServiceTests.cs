using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services;
using Moq;
using NUnit.Framework;

namespace DrynksMes.Services.Tests.Services
{
    [TestFixture]
    public class MerchantServiceTests
    {
        private Mock<IDatabaseContext> _databaseContext;
        private Mock<MerchantService> _merchantService;
        private Merchant _merchantOne;
        private Merchant _merchantTwo;

        [SetUp]
        public void SetUp()
        {
            _databaseContext = new Mock<IDatabaseContext>();
            //_databaseContext.Setup(x => x.GetDatabase()).Returns(new DrynksDb
            //    {
            //        Merchants = new Database<DrynksDb>.Table<Merchant>(new DrynksDb(), "")
            //    });
           // _databaseContext.As<IDisposable>().Setup(x=>x.Dispose());

            _merchantService = new Mock<MerchantService>(_databaseContext.Object);

            _merchantOne = new Merchant { PrimaryContact = "FakeContact1", Id = 1 };
            _merchantTwo = new Merchant { PrimaryContact = "FakeContact2", Id = 2 };
        }

        [Test]
        public void GetVenues_OnAccess_GetsVenues()
        {
            
            //_merchantService.Setup(x => x.GetMerchants()).Returns(new List<Merchant>
            //    {
            //        _merchantOne,
            //        _merchantTwo,
            //    });
            //var result = _merchantService.Object.GetVenues().ToList();
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result[0].Id,1);
            //Assert.AreEqual(result[1].Id,2);
            //_merchantService.VerifyAll();
        }

        [Test]
        public void AddVenue_OnPassingVenue_AddsVenueToDatabase()
        {
            //_merchantService.Setup(x => x.AddMerchant(_merchantOne)).Returns(1);
            //var result = _merchantService.Object.AddVenue(_merchantOne);   
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result,1);
            //_merchantService.VerifyAll();
        }

        [Test]
        public void UpdateVenue_OnPassingVenue_UpdatesVenue()
        {
            //_merchantService.Setup(x => x.UpdateMerchant(_merchantOne)).Returns(1);
            //var result = _merchantService.Object.UpdateVenue(_merchantOne);
            //Assert.AreNotEqual(result,0);
            //Assert.AreEqual(result,1);
        }

        [Test]
        public void GetVenue_OnPassingVenueId_GetVenu()
        {
            //const int id = 1;
            //_merchantService.Setup(x => x.GetMerchant(id)).Returns(_merchantOne);
            //var venue = _merchantService.Object.GetVenue(id);
            //Assert.IsNotNull(venue);
            //Assert.IsInstanceOf(typeof(Merchant),venue);
            //Assert.AreEqual(venue.Id,1);


        }
    }
}
