using identity_testing.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace identity_testing.Pages
{
    public class LoggedinModel : PageModel
    {
        private UserManager<Users> userManager { get; }
        private SignInManager<Users> signInManager { get; }

        private readonly ILogger<IndexModel> _logger;

        public LoggedinModel(ILogger<IndexModel> logger, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string User_Id { get; set; }

        public IActionResult OnGet()
        {
            if (signInManager.IsSignedIn(User))
            {
                User_Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Page();
            }
            else
            {
                //return ("Signin");
                return Redirect("Signin");
            }
        }

    }
}
