using System.ComponentModel.DataAnnotations;

namespace WEB_API.DAL.ViewModels
{
    public class PasswordChangingViewModel
    {
        [Required(ErrorMessage = "Password required")]
        [MaxLength(50)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Invalid old password")]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "Password required")]
        [MaxLength(50)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Invalid new password")]
        public string NewPassword { get; set; }
    }
}