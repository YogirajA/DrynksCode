using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DrynksMe.DataAccess.Models;
using DrynksMe.Portal.Models;
using DrynksMe.Services.Contracts;
using PagedList;

namespace DrynksMe.Portal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VenueController :  BaseController
    {
       
        public VenueController(IMembershipService membershipService, 
                                IMerchantService merchantService, 
                                IDrinksServices drinksServices) 
            : base(membershipService, merchantService, drinksServices){}


        public ActionResult Index()
        {
           
            return View();
        }

        
        public async  Task<ActionResult> AllVenues()
        {
            var venues =  await  AllVenuesFromDb();  //MerchantService.GetVenues();
            
            return View(venues);
        }

        

        public ActionResult Search(int? page,VenueSearchModel venueSearchModel)
        {
            VenueSearch(page, venueSearchModel);
            return View(venueSearchModel);
        }

        public  ActionResult GetVenues()
        {
            var venues =  AllVenuesFromDb();
            return Json(venues, JsonRequestBehavior.AllowGet);
        }

        private async Task<IEnumerable<VenueModel>> AllVenuesFromDb()
        { 
            
            var allVenuesFromDb = await Task.FromResult(MerchantService.GetVenues());
                                  
            return await Task.FromResult(allVenuesFromDb.Select(x=>GetVenueFromMerchant(x)));
               
        }

        public ActionResult Add()
        {
            var venueModel = new VenueModel
                {
                    MerchantTypes = GetSelectedItemsForMerchantType(MerchantService.GetAllMerchantTypes().ToList())
                };

            return View(venueModel);
        }

        [HttpPost]
        public ActionResult Add(VenueModel venueModel)
        {
            if (ModelState.IsValid)
            {
                var merchant = GetMerchantFromVenue(venueModel);
                merchant.CreateDt = DateTime.Now;
                merchant.UpdateDt = DateTime.Now;
                var merchantTypeIds = venueModel.MerchantTypeIds.ToArray();
                var venueId = MerchantService.AddVenue(merchant, merchantTypeIds); // fixing it
                ViewBag.IsSuccessful = true;
                return RedirectToActionPermanent("Edit", new { @id = venueId });
            }
            ViewBag.IsSuccessful = false;
            return View(venueModel);
        }

        public ActionResult Edit(int? id)
        {
            Merchant merchant = null;
            var allMerchantTypes=Enumerable.Empty<MerchantType>();
            var merchantTypesForMerchant = Enumerable.Empty<MerchantType>();
            if (id.HasValue)
            {
                var merchantGroup = MerchantService.GetVenue(id.Value);
                merchant = merchantGroup.Merchant;
                allMerchantTypes = merchantGroup.AllMerchantTypes;
                merchantTypesForMerchant = merchantGroup.MerchantTypeForMerchants;

            }
            var venueModel = GetVenueFromMerchant(merchant,allMerchantTypes,merchantTypesForMerchant);
            return View(venueModel);
        }

        [HttpPost]
        public ActionResult Edit(VenueModel venueModel)
        {   
            if (ModelState.IsValid)
            {
                var merchant = GetMerchantFromVenue(venueModel);
                merchant.UpdateDt = DateTime.Now;
                MerchantService.UpdateVenue(merchant,venueModel.MerchantTypeIds.ToArray());//fixing it
                ViewBag.IsSuccessful = true;
                return View(venueModel);
            }
            ViewBag.IsSuccessful = false;
            return View(venueModel);
        }

        private Merchant GetMerchantFromVenue(VenueModel venueModel)
        {
            var merchantFromVenue = new Merchant();

            if (venueModel == null)
                return merchantFromVenue;


            merchantFromVenue.Id = venueModel.Id;
            merchantFromVenue.MerchantName = venueModel.VenueName;
            merchantFromVenue.ProfileName = venueModel.ProfileName;
            merchantFromVenue.Description = venueModel.Description;
            merchantFromVenue.Location = venueModel.Location;
            merchantFromVenue.PrimaryContact = venueModel.PrimaryContact;
            merchantFromVenue.ContactNumber = venueModel.ContactNumber;
            merchantFromVenue.CustomerNumber = venueModel.CustomerNumber;
            merchantFromVenue.CreateDt = venueModel.CreateDt;
            merchantFromVenue.UpdateDt = venueModel.UpdateDt;

            merchantFromVenue.ShortVenueInformation = venueModel.ShortVenueInformation;
            merchantFromVenue.AddressInfo = venueModel.AddressInfo;
            merchantFromVenue.City = venueModel.City;
            merchantFromVenue.State = venueModel.State;
            merchantFromVenue.Zip = venueModel.Zip;
            merchantFromVenue.TwitterHandle = venueModel.TwitterHandle;
            merchantFromVenue.Website = venueModel.Website;
            merchantFromVenue.VenueType = venueModel.VenueType;

            merchantFromVenue.ImageLarge = venueModel.ImageLarge;
            merchantFromVenue.ImageMedium = venueModel.ImageMedium;
            merchantFromVenue.ImageSmall = venueModel.ImageSmall;

            merchantFromVenue.Email = venueModel.Email;
            merchantFromVenue.IsActive = venueModel.IsActive;

            merchantFromVenue.Longitude = venueModel.Longitude;
            merchantFromVenue.Latitude = venueModel.Latitude;
            return merchantFromVenue;
        }

        private VenueModel GetVenueFromMerchant(Merchant merchant,IEnumerable<MerchantType> allMerchantTypes=null, IEnumerable<MerchantType> merchantTypesForMerchant=null)
        {
            var venueFromMerchant = new VenueModel();
            if (merchant == null)
                return venueFromMerchant;


            venueFromMerchant.Id = merchant.Id;
            venueFromMerchant.VenueName = merchant.MerchantName;
            venueFromMerchant.ProfileName = merchant.ProfileName;
            venueFromMerchant.Description = merchant.Description;
            venueFromMerchant.Location = merchant.Location;
            venueFromMerchant.PrimaryContact = merchant.PrimaryContact;
            venueFromMerchant.ContactNumber = merchant.ContactNumber;
            venueFromMerchant.CustomerNumber = merchant.CustomerNumber;
            venueFromMerchant.CreateDt =  merchant.CreateDt;
            venueFromMerchant.UpdateDt = merchant.UpdateDt;

            venueFromMerchant.ShortVenueInformation = merchant.ShortVenueInformation;
            venueFromMerchant.AddressInfo = merchant.AddressInfo;
            venueFromMerchant.City = merchant.City;
            venueFromMerchant.State = merchant.State;
            venueFromMerchant.Zip = merchant.Zip;
            venueFromMerchant.TwitterHandle = merchant.TwitterHandle;
            venueFromMerchant.Website = merchant.Website;
            venueFromMerchant.VenueType = merchant.VenueType;


            venueFromMerchant.ImageLarge = merchant.ImageLarge;
            venueFromMerchant.ImageMedium = merchant.ImageMedium;
            venueFromMerchant.ImageSmall = merchant.ImageSmall;
            venueFromMerchant.Email = merchant.Email;
            
            venueFromMerchant.IsActive = merchant.IsActive;

            venueFromMerchant.Longitude = merchant.Longitude;
            venueFromMerchant.Latitude = merchant.Latitude;

            if(allMerchantTypes!=null&&merchantTypesForMerchant!=null)
                venueFromMerchant.MerchantTypes = GetSelectedItemsForMerchantType(allMerchantTypes,merchantTypesForMerchant) ;
            return venueFromMerchant;

        }

        private IEnumerable<SelectListItem> GetSelectedItemsForMerchantType(IEnumerable<MerchantType> allMerchantTypes, IEnumerable<MerchantType> merchantTypesForMerchant=null)
        {
            return allMerchantTypes.Select(x =>
                                    new SelectListItem
                                        {
                                            Selected = merchantTypesForMerchant!=null && merchantTypesForMerchant.Select(s=>s.Id).Contains(x.Id),
                                            Text = x.TypeDescription,
                                            Value = x.Id.ToString(CultureInfo.InvariantCulture)
                                        }).ToList();
        }

        private void VenueSearch(int? page, VenueSearchModel venueSearchModel)
        {
            var pageNumber = (page ?? 1);
            const int pageSize = 10;
            var startRowNum = (pageNumber - 1) * pageSize;
            var endRowNum = startRowNum + pageSize;
            var merchantSearchModel = new MerchantSearchModel
                {
                StartRowNum = startRowNum,
                EndRowNum = endRowNum,
                MerchantName =  venueSearchModel.MerchantName,
                ProfileName = venueSearchModel.ProfileName,
                TwitterHandle = venueSearchModel.TwitterHandle,
                VenueType = venueSearchModel.VenueType
            };
            var merchants = MerchantService.Search(merchantSearchModel).ToList();
            var firstMerchantRecord = merchants.Any() ? merchants.FirstOrDefault() : null;
            if (firstMerchantRecord != null)
            {
                var total = firstMerchantRecord.Total;
                var venues = merchants.Select(x=>GetVenueFromMerchant(x)).ToList();
                var staticlist = new StaticPagedList<VenueModel>(venues, pageNumber, pageSize, total);
                venueSearchModel.VenuesPaged = staticlist;
            }
        }

    }

   
}
