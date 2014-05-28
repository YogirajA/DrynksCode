using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrynksMe.DataAccess.Models;

namespace DrynksMe.Services.Api.Models
{
    public class VenueModel
    {
       
        public int Id { get; set; }
        public string MerchantName { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PrimaryContact { get; set; }
        public string ContactNumber { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime? CreateDt { get; set; }
        public DateTime? UpdateDt { get; set; }
        public string ShortVenueInformation { get; set; }
        public string AddressInfo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string TwitterHandle { get; set; }
        public string Website { get; set; }
        public string VenueType { get; set; }
        public string ImageSmall { get; set; }
        public string ImageLarge { get; set; }
        public string ImageMedium { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public bool IsPremierMerchant { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public IEnumerable<UserMerchantComment> Comments { get; set; }
    }

    public class VenueComment
    {
        public int VenueId { get; set; }
        public string Comment { get; set; }
    }
}