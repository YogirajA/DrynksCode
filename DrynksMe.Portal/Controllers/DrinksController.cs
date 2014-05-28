using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Contracts;

namespace DrynksMe.Portal.Controllers
{
    public class DrinksController : BaseController
    {
        //
        // GET: /Drinks/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public DrinksController(IMembershipService membershipService, IMerchantService merchantService, IDrinksServices drinksServices) : base(membershipService, merchantService, drinksServices)
        {
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult All()
        {
            return View();
        }

        public ActionResult Search()
        {
           //var t = DrinksServices.GetAllDrinks();
           // var t2 = new List<Drink>();
           // Parallel.ForEach(t, t2.Add);
            return View();
        }

    }
}
