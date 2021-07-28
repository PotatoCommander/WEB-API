using System;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.BusinessModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genres Genre { get; set; }
        public AgeRatings AgeRating { get; set; }
        public decimal Price { get; set; }
        public Categories Category { get; set; }
        public DateTime CreationTime { get; set; }
        public int YearOfProduction { get; set; }
        public float Rating { get; set; }
        public uint Count { get; set; }
    }
}