using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Api.Models;
using DrynksMe.Services.Models;

namespace DrynksMe.Services.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector),
                                                           new HeaderVersionControllerSelector(GlobalConfiguration.Configuration));

            GlobalConfiguration.Configuration.Filters.Add(new ApiAuthorize());
            GlobalConfiguration.Configuration.Filters.Add(new CustomHttpsAttribute());
            CreateMaps();
        }


        private void CreateMaps()
        {
            //Mapper.CreateMap("Source","Dest")
            Mapper.CreateMap<DrinksModel, Drink>();
            Mapper.CreateMap<DrinksResultModel, DrinksModel>()
                  .ForMember(d => d.RowNumber, s => s.MapFrom(d => d.RowNumber))
                  .ForMember(d => d.Tags, s => s.MapFrom(d => new List<string>{d.TagName}))
                  .ForMember(d => d.IsLiked, s => s.MapFrom(d => (d.IsLiked.HasValue && d.IsLiked.Value)))
                  .ForMember(d => d.Notes, s => s.MapFrom(d => d.UserNotes))
                  .ForMember(d => d.Total, s => s.MapFrom(d => d.Total));
            Mapper.CreateMap<Drink, DrinksModel>();
            Mapper.CreateMap<UserModel, User>()
                  .ForMember(d => d.Share, s => s.MapFrom(d => !d.Share.HasValue || d.Share.Value));

            Mapper.CreateMap<Merchant, VenueModel>();
            Mapper.CreateMap<MerchantWithComments, VenueModel>();
                  //.ForMember(d=>d.Comments,s=>s.MapFrom(d=>d.Comments))
                  //.ForMember(d=>d.Id);
                  
            Mapper.CreateMap<User, UserModel>();
        }

    }
}