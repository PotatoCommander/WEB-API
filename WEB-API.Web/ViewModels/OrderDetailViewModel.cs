using System;
using System.ComponentModel.DataAnnotations;

namespace WEB_API.Web.ViewModels
{
    public class OrderDetailViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
    }
}