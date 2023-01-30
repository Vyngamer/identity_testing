using identity_testing.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace identity_testing.Pages
{
    public class IndexModel : PageModel
    {
        private UserManager<Users> userManager { get; }
        private SignInManager<Users> signInManager { get; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string User_Id { get; set; }
        public string full_name { get; set; }
        public string credit_card_no { get; set; }
        public string gender { get; set; }
        public string mobile_no { get; set; }
        public string delivery_address { get; set; }
        public string email { get; set; }
        public string image_string { get; set; }
        public string about_me { get; set; }
        public bool signed_in { get; set; }

        public void OnGet()
        {
            if (signInManager.IsSignedIn(User))
            {
                User_Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                full_name = userManager.FindByIdAsync(User_Id).Result.full_name;
                gender = userManager.FindByIdAsync(User_Id).Result.gender;
                mobile_no = userManager.FindByIdAsync(User_Id).Result.mobile_no;
                //delivery_address = HttpUtility.HtmlEncode(RModel.delivery_address)
                delivery_address = HttpUtility.HtmlDecode(userManager.FindByIdAsync(User_Id).Result.delivery_address);
                email = userManager.FindByIdAsync(User_Id).Result.Email;
                image_string = userManager.FindByIdAsync(User_Id).Result.ImageURL;
                about_me = HttpUtility.HtmlDecode(userManager.FindByIdAsync(User_Id).Result.about_me);
                signed_in = true;
                var dataprotectionprovider = DataProtectionProvider.Create("EncryptData");
                var protector = dataprotectionprovider.CreateProtector("MySecretKey");
                var decrypted_cc = protector.Unprotect(userManager.FindByIdAsync(User_Id).Result.credit_card_no);
                credit_card_no = decrypted_cc;

                //decryption of card info thingi
                //credit_card_no = userManager.FindByIdAsync(User_Id).Result.credit_card_no;
            }
        }
    }
}