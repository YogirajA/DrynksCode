using DrynksMe.DataAccess.Models;

namespace DrynksMe.Services.Models
{
    public class DrinksFilterModel
    {
        public int StartRowNum { get; set; }
        public int EndRowNum { get; set; }
        public int UserId { get; set; }
        public string TagName { get; set; }
        public string DeviceId { get; set; }

    }

    public class DrinksResultModel : Drink
    {
        public bool? IsLiked { get; set; }
        public string UserNotes { get; set; }
        public string TagName { get; set; }
        public int Total { get; set; }
        public int RowNumber { get; set; }
        public bool IsSignature { get; set; }
    }
}
