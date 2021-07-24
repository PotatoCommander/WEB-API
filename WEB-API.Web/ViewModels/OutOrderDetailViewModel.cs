using System.ComponentModel.DataAnnotations;

namespace WEB_API.Web.ViewModels
{
    public class OutOrderDetailViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}