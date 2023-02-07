using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AceJobAgency.ViewModels;
using AceJobAgency.Services;
using AceJobAgency.Model;
using Microsoft.AspNetCore.DataProtection;
using AceJobAgency.Service;

namespace AceJobAgency.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<AceJobUser> userManager { get; }
        private SignInManager<AceJobUser> signInManager { get; }

        private RoleManager<IdentityRole> roleManager { get; }

        private readonly MembershipService _memberService;
        private readonly GoogleCaptchaService _captchaService;


        private IWebHostEnvironment _environment;

        [BindProperty]
        public Membership RModel { get; set; }

        public RegisterModel(UserManager<AceJobUser> userManager,
        SignInManager<AceJobUser> signInManager, 
        RoleManager<IdentityRole>roleManager,
        MembershipService memberService, 
        GoogleCaptchaService captchaService,
        IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _memberService = memberService;
            _captchaService = captchaService;
            _environment = environment;
        }

        [BindProperty]
        public IFormFile? Resume { get; set; }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var captchaResult = await _captchaService.VerifyToken(RModel.Token);
            // CAPTCHA //
            // CAPTCHA //
            // To show working, remove ! //
            if (!captchaResult) { return Page(); }


            if (ModelState.IsValid)
            {
                if (Resume != null)
                {
                    if (Resume.Length > (2 * 1024 * 1024))
                    {
                        ModelState.AddModelError("Resume", "File size cannot exceed 2MB");
                    }

                    var uploadsFolder = "resumes";
                    var resumeFile = Guid.NewGuid() + Path.GetExtension(Resume.FileName);
                    var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, resumeFile);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await Resume.CopyToAsync(fileStream);
                    RModel.ResumeURL = string.Format("/{0}/{1}", uploadsFolder, resumeFile);
                }

                IdentityRole role = await roleManager.FindByIdAsync("Member");
                if (role == null)
                {
                    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Member"));
                    if (!result2.Succeeded)
                    {
                        ModelState.AddModelError("", "Create member role failed");
                    }
                }

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("entrenchedHowitzer");

                var user = new AceJobUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FName = protector.Protect(RModel.FName),
                    LName = protector.Protect(RModel.LName),
                    Gender = protector.Protect(RModel.Gender),
                    NRIC = protector.Protect(RModel.NRIC),
                    ResumeURL = protector.Protect(RModel.ResumeURL),
                    DOB = protector.Protect(RModel.DOB.ToString()),
                    Whoami = protector.Protect(RModel.Whoami),
                    Token = protector.Protect(RModel.Token)
                };

                
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
     
                    result = await userManager.AddToRoleAsync(user, "Member");

                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Sign-up successful!");
                    return RedirectToPage("/Login");
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
