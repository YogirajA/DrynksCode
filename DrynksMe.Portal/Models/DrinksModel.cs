using System;
using System.ComponentModel.DataAnnotations;

namespace DrynksMe.Portal.Models
{
    public class DrinksModel
    {
        
        public int Id { get; set; }
        public string DrinkName { get; set; }
        public string DrinkDescription { get; set; }
        public string FoundAt { get; set; }
        public string Origin { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreateDt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UpdateDt { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageSmall { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageLarge { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageMedium { get; set; }
        [DataType(DataType.ImageUrl)]
        public string StandardPicUrl { get; set; }
        public decimal? AlcoholContent { get; set; }

    }
}