namespace DrynksMe.DataAccess.Models
{
    public class MerchantSearchModel
    {
        
        public string ProfileName { get; set; }
        public string MerchantName { get; set; }

        public string VenueType { get; set; }

        public string TwitterHandle { get; set; }

        public int StartRowNum { get; set; }
        public int EndRowNum { get; set; }
    }
}
