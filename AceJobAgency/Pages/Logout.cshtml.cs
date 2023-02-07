using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using AceJobAgency.Model;


namespace AceJobAgency.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AceJobUser> signInManager;
        public LogoutModel(SignInManager<AceJobUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet() { }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("Login");
        }
        public async Task<IActionResult> OnPostDontLogoutAsync()
        {
            return RedirectToPage("/Member/MemberIndex");
        }
    }
}
