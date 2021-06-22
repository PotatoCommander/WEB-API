﻿using System;
using System.Collections.Generic;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genres Genre { get; set; }
        public AgeRatings AgeRating { get; set; }
        public decimal Price { get; set; }
        public Categories Category { get; set; }
        public DateTime CreationTime { get; set; }
        public float Rating { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}