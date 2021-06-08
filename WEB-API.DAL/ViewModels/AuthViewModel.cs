﻿using System.ComponentModel.DataAnnotations;

namespace WEB_API.DAL.ViewModels
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password required")]
        [MaxLength(50)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; }
    }
}