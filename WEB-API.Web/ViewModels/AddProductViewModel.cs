﻿using System;
using System.ComponentModel.DataAnnotations;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Web.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [EnumDataType(typeof(Genres), ErrorMessage = "Invalid genres Enum value.")]
        public Genres Genre { get; set; }
        [EnumDataType(typeof(AgeRatings), ErrorMessage = "Invalid ageRating Enum value.")]
        public AgeRatings AgeRating { get; set; }
        [Required]
        public decimal Price { get; set; }
        [EnumDataType(typeof(Categories), ErrorMessage = "Invalid category Enum value.")]
        public Categories Category { get; set; }
    }
}