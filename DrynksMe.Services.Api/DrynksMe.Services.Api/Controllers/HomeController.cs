using System.Configuration;
using System.Web.Mvc;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.Services.Contracts;

namespace DrynksMe.Services.Api.Controllers
{
    public class HomeController : Controller
    {
        private IDrinksServices _drinksServices;
       

        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Index()
        //{

        //    _drinksServices =
        //           new DrinksServices(
        //               new DatabaseContext(ConfigurationManager.ConnectionStrings["DrynksMeDBEntities"].ConnectionString));


        //    var d = _drinksServices.GetAllDrinks();
        //    return View(d);
            
        //}
    }
}
