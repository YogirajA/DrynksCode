using System;

namespace DrynksMe.Services.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string DeviceId { get; set; }
        public string TwitterId { get; set; }
        public string FacebookId { get; set; }
        public DateTime? CreateDt { get; set; }
        public DateTime? UpdateDt { get; set; }
        public string TwitterPicUrl { get; set; }
        public bool? Share { get; set; }
        public string AnonymousSystemId { get; set; }
        //public int UserProfileId { get; set; }
    }
}