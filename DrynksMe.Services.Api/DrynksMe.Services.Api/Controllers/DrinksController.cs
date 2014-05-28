using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Api.Models;
using DrynksMe.Services.Contracts;
using DrynksMe.Services.Logging;
using DrynksMe.Services.Models;

namespace DrynksMe.Services.Api.Controllers
{
    public class DrinksController : BaseApiController
    {
        public DrinksController(IMembershipService membershipService, IDrinksServices drinksServices, ILogger logger, IMerchantService merchantService) : base(membershipService, drinksServices, logger, merchantService)
        {
        }

      
        public DrinksModel GetById(int id)
        {  
             var drinkModel =  DrinksServices.GetDrinkById(id).ToDrinksModel();
             return drinkModel;
        }

        public IEnumerable<DrinksModel> GetByUser(string tagName)
        {
            return GetByUser(tagName, 1);
        }

        public IEnumerable<DrinksModel> GetByUser(string tagName, int? page, int pageSize = 10)
        {
            var drinksFilters = GetDrinksFilters(null, CurrentUser.Id, page, pageSize, tagName);
            return DrinksServices.GetDrinksByUserIdPaged(drinksFilters).Select(drink => drink.ToDrinksModel()).ToList();

        }
        //either add the name get in the call or add attr.
        [HttpGet]
        public IEnumerable<DrinksModel> Explore(string tagName)
        {
            return Explore(tagName, 1);
        }
        [HttpGet]
        public IEnumerable<DrinksModel> Explore(string tagName, int? page, int pageSize = 10)
        {
            var drinksFilters = GetDrinksFilters(null, CurrentUser.Id, page, pageSize, tagName);
            return DrinksServices.GetDrinksPaged(drinksFilters).Select(drink => drink.ToDrinksModel()).ToList();

        }

        [HttpPost]
        public HttpResponseMessage Insert([FromBody]DrinksModel drinksModel)
        {
            if (ModelState.IsValid && drinksModel != null)
            {
                var drink = drinksModel.ToDrink();
                drink.IsShared = CurrentUser.Share;
                var tags = drinksModel.Tags;
                var drinkUser = new DrinkUser {UserId = CurrentUser.Id,IsLiked = drinksModel.IsLiked,UserNotes = drinksModel.Notes};
                var drinkFromDb = DrinksServices.AddDrinkForUser(drink, drinkUser,tags);
                return Request.CreateResponse(HttpStatusCode.Created, drinkFromDb.ToDrinksModel());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }


        [HttpPut]
        public DrinksModel Update([FromBody]DrinksModel drinksModel)
        {
            var drink = drinksModel.ToDrink();
            drink.IsShared = CurrentUser.Share;
            var tags = drinksModel.Tags;
            var drinkUser = new DrinkUser {UserId = CurrentUser.Id,  IsLiked = drinksModel.IsLiked, UserNotes = drinksModel.Notes };
            return DrinksServices.EditDrink(drink,drinkUser, tags).ToDrinksModel();
            
        }

       
        [HttpPost]
        public void Delete(int id)
        {
            if (null == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }


        private static DrinksFilterModel GetDrinksFilters(string deviceId, int userId, int? page, int pageSize, string tagName)
        {
            var pageNumber = (page ?? 1);
            var startRowNum = (pageNumber - 1) * pageSize;
            var endRowNum = startRowNum + pageSize;
            var drinksFilters = new DrinksFilterModel
            {
                StartRowNum = startRowNum,
                EndRowNum = endRowNum,
                DeviceId = deviceId,
                UserId = userId,
                TagName = tagName
            };
            return drinksFilters;
        }
    }

    
}
