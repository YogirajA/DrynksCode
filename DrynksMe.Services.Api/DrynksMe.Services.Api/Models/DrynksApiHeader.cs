using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrynksMe.Services.Api.Models
{
    public class DrynksApiHeader
    {   
        public string AnonymousSystemId { get; set; }
        public string DeviceId { get; set; }
        public string TwitterId { get; set; }
        public int UserId { get; set; }
        public string ApiKey { get; set; }
        public string RequestTime { get; set; }
    }
}