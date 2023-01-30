using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using identity_testing.ViewModel;
using identity_testing.Model;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace identity_testing.Pages
{
    public class SignupModel : PageModel
    {
        [BindProperty]
        public Signup RModel { get; set; }

        [BindProperty]
        public IFormFile image_source { get; set; }


        private UserManager<Users> userManager { get; }
        private SignInManager<Users> signInManager { get; }

        private IWebHostEnvironment _environment;

        public SignupModel(UserManager<Users> userManager,SignInManager<Users> signInManager, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var image_string = "";
            if (ModelState.IsValid)
            {
                if (image_source != null)
                {
                    if (image_source.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }
                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(image_source.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var filestream = new FileStream(imagePath, FileMode.Create);
                    await image_source.CopyToAsync(filestream);
                    image_string = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }
                var dataprotectionprovider = DataProtectionProvider.Create("EncryptData");
                var protector = dataprotectionprovider.CreateProtector("MySecretKey");
                var user = new Users()
                {
                    Email = RModel.Email,
                    UserName = RModel.Email,
                    full_name = RModel.full_name,
                    //credit_card_no = RModel.credit_card_no,
                    credit_card_no = protector.Protect(RModel.credit_card_no),
                    gender = RModel.gender,
                    mobile_no = RModel.mobile_no,
                    delivery_address = HttpUtility.HtmlEncode(RModel.delivery_address),
                    ImageURL = image_string,
                    //about_me = RModel.about_me
                    about_me = HttpUtility.HtmlEncode(RModel.about_me)
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
