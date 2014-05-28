using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using DrynksMe.Services.Api.Models;
using DrynksMe.Services.Contracts;
using DrynksMe.Services.Logging;

namespace DrynksMe.Services.Api.Controllers
{
    
    public class DrinksControllerV2 : BaseApiController
    {
        public DrinksControllerV2(IMembershipService membershipService, IDrinksServices drinksServices, ILogger logger, IMerchantService merchantService) : base(membershipService, drinksServices, logger, merchantService)
        {
        }

        //public IEnumerable<DrinksModel> GetAll()
        //{   
        //    return DrinksServices.GetAllDrinks().Select(drink=>drink.ToDrinksModel()).ToList();
        //}

      

        //public DrinksModel GetById(int id)
        //{  
        //     var drinkModel =  DrinksServices.GetDrinkById(id).ToDrinksModel();
        //     return drinkModel;
        //}

        //// GET api/drynks/5
        //public IEnumerable<DrinksModel> GetByDeviceId(string id)
        //{
        //    return DrinksServices.GetDrinksForUserByDeviceId(id).Select(drink=>drink.ToDrinksModel()).ToList();
           
        //}

        //public IEnumerable<DrinksModel> GetByUserId(int id)
        //{
            
        //    return DrinksServices.GetDrinksForUserByUserId(id).Select(drink => drink.ToDrinksModel()).ToList();

        //}


        //[HttpPost]
        //public HttpResponseMessage PostDrinkByUserId([FromUri]int userId, [FromBody]DrinksModel drinksModel)
        //{
        //    if (ModelState.IsValid && drinksModel != null)
        //    {
        //        var drink = drinksModel.ToDrink();
        //        var drinkId = DrinksServices.AddDrinkForUser(drink, userId);
        //        return GetHttpResponseMessage(drinksModel, drinkId);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.BadRequest);
        //}

        //[HttpPost]
        //public HttpResponseMessage PostDrinkByDeviceId([FromUri]string deviceId,[FromBody] DrinksModel drinksModel)
        //{
        //    if (ModelState.IsValid && drinksModel != null)
        //    {
        //        var drink = drinksModel.ToDrink();
        //        var drinkId = DrinksServices.AddDrinkForDevice(drink, deviceId);
        //        drinksModel.Id = drinkId;
        //        return GetHttpResponseMessage(drinksModel, drinkId);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.BadRequest);
        //}

        //private HttpResponseMessage GetHttpResponseMessage(DrinksModel drinksModel, int drinkId)
        //{
        //    var response = Request.CreateResponse(HttpStatusCode.Created, drinksModel);
        //    response.Headers.Location = GetLocation(drinkId);
        //    return response;
        //}

        //// POST api/drynks
        //[HttpPut]
        //public DrinksModel PutDrink([FromBody]DrinksModel drinksModel)
        //{
        //    var drink = drinksModel.ToDrink();
        //    DrinksServices.EditDrink(drink);
        //    return drinksModel;
        //}

        //// DELETE api/drynks/5
        //public void Delete(int id)
        //{
        //    if (null == null)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        //    }
        //}

        //Uri GetLocation(int id)
        //{
        //    var controller = Request.GetRouteData().Values["controller"];
        //    return new Uri(Url.Link("DefaultApi", new { controller = controller, id = id }));
        //}
    }
}
