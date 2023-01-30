using identity_testing.Model;
using identity_testing.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace identity_testing.Pages
{
    public class SigninModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private UserManager<Users> userManager { get; }
        private SignInManager<Users> signInManager { get; }
        public Signup RModel { get; set; }
        public SigninModel(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, true, true);
                if (identityResult.Succeeded)
                {
                    return RedirectToPage("Index");
                }
/*                else
                {
                    ModelState.AddModelError("", "Username or Password incorrect");
                }*/
                if (identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account has been locked out, please try again in 25 seconds");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page();
        }
    }
}
