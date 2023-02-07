using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AceJobAgency.ViewModels;
using AceJobAgency.Services;
using AceJobAgency.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AceJobAgency.Pages
{
    public class LoginModel : PageModel
    {

        private UserManager<AceJobUser> userManager { get; }
        private SignInManager<AceJobUser> signInManager { get; }

        private MembershipService _memberService { get; }

        private static readonly HttpClient client = new HttpClient();

        [BindProperty]
        public Login LModel { get; set; }

        public LoginModel(UserManager<AceJobUser> userManager,
        SignInManager<AceJobUser> signInManager,
        MembershipService memberService, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _memberService = memberService;
        }

      
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, true);
                if (identityResult.Succeeded)
                {
                    var userNRIC = _memberService.GetAceJobUserByEmail(LModel.Email).NRIC;
                  
                    HttpContext.Session.SetString("NRIC", userNRIC.ToString());
                    return RedirectToPage("/Member/MemberIndex");
                }
                else if (identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is locked out. Kindly wait for 10 minutes and try again");
                }

                else
                {
                    ModelState.AddModelError("", "Username or Password incorrect");
                }
            }
            return Page();
        }







    }
}
