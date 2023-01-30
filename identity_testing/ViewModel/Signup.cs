using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace identity_testing.ViewModel
{
    public class Signup
    {
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required, RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Full name can have letters only")]
        [Display(Name = "Full Name")]
        public string full_name { get; set; } = string.Empty;

        [Required, RegularExpression(@"^[a-zA-z0-9#?!@$%^&*-]{8,}$", ErrorMessage = "Password must be at least 8 Characters long, have one uppercase and lower case letter, have at least one number and one special character")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [Required, RegularExpression(@"^(?=.*?[0-9]).{16}$", ErrorMessage = "Credit card format is wrong, only enter 16 digits")]
        [Display(Name = "Credit Card No.")]
        public string credit_card_no { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; } = string.Empty;

        [Required, RegularExpression(@"^(?=.*?[0-9]).{8}$", ErrorMessage = "Phone number can have numbers only and must be 8 numbers long")]
        [Display(Name = "Mobile No.")]
        public string mobile_no { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Delivery Address")]
        public string delivery_address { get; set; } = string.Empty;

        /*        [Display(Name = "Email")]
                public string email_address { get; set; } = string.Empty;*/

        [RegularExpression(@"[^\\s]+(.*?)\\.(jpg|JPG)$", ErrorMessage = "photo can only be in jpg format")]
        [MaxLength(50)]
        public string? ImageURL { get; set; }

        [Required]
        [Display(Name = "About Me")]
        public string about_me { get; set; } = string.Empty;
    }
}
