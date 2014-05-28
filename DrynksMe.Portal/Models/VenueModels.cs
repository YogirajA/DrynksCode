using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DrynksMe.Portal.Models
{
    public class VenueModel 
    {
        public VenueModel()
        {
            MerchantTypes = new List<SelectListItem>();
        }
        public int Id { get; set; }
        [DisplayName("Venue Name")]
        public string VenueName { get; set; }
        [DisplayName("Profile Name")]
        public string ProfileName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; }
        [DisplayName("Primary Contact")]
        public string PrimaryContact { get; set; }
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        [DisplayName("Customer Number")]
        public string CustomerNumber { get; set; }
        [DisplayName("Short Venue Description")]
        public string ShortVenueInformation { get; set; }
        [DisplayName("More Information about Address")]
        public string AddressInfo { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Zip")]
        public string Zip { get; set; }
        [DisplayName("Twitter Handle")]
        public string TwitterHandle { get; set; }
        [DisplayName("Website Address")]
        public string Website { get; set; }
        [DisplayName("Venue type(comma separated)")]
        public string VenueType { get; set; }

        public DateTime? CreateDt { get; set; }
        public DateTime? UpdateDt { get; set; }

        [DisplayName("Large Image Address")]
        public string ImageLarge { get; set; }
        [DisplayName("Medium Image Address")]
        public string ImageMedium { get; set; }
        [DisplayName("Small Image Address")]
        public string ImageSmall { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Venue Type")]
        public IEnumerable<SelectListItem> MerchantTypes { get; set; }

        public IEnumerable<int> MerchantTypeIds { get; set; }
        [DisplayName("Longitude")]
        [DisplayFormat(DataFormatString = "{0:#,##0.000000#}", ApplyFormatInEditMode = true)]
        public decimal? Longitude { get; set; }
        [DisplayName("Latitude")]
        [DisplayFormat(DataFormatString = "{0:#,##0.000000#}", ApplyFormatInEditMode = true)]
        public decimal? Latitude { get; set; }
    }
}