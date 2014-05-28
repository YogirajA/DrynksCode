using System.ComponentModel;
using PagedList;

namespace DrynksMe.Portal.Models
{
    public class VenueSearchModel
    {
        public VenueSearchModel()
        {
            VenuesPaged = new PagedList<VenueModel>(null,1,1);
        }

        public int? Page { get; set; }
        [DisplayName("Profile Name")]
        public string ProfileName { get; set; }
        [DisplayName("Venue Name")]
        public string MerchantName { get; set; }

        [DisplayName("Venue Type")]
        public string VenueType { get; set; }

        [DisplayName("Twitter Handle")]
        public string TwitterHandle { get; set; }


        public IPagedList<VenueModel> VenuesPaged { get; set; }
    }
}