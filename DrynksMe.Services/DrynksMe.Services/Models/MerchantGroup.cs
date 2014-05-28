using System;
using System.Collections.Generic;
using DrynksMe.DataAccess.Models;

namespace DrynksMe.Services.Models
{
    public class MerchantGroup
    {
        public Merchant Merchant { get; set; }
        public IEnumerable<MerchantType> MerchantTypeForMerchants { get; set; }
        public IEnumerable<MerchantType> AllMerchantTypes { get; set; }
    }

    public class MerchantWithComments
    {
        public Merchant Merchant { get; set; }
        public IEnumerable<UserMerchantComment> Comments { get; set; } 
    }

}