using AceJobAgency.Model;
using AceJobAgency.Services;
using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AceJobAgency.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        private readonly MembershipService _memberService;

        public IndexModel(ILogger<IndexModel> logger, MembershipService memberService)
        {
            _memberService = memberService;
            _logger = logger;
        }

        public List<AceJobUser> MembershipList { get; set; }
        public void OnGet()
        {
            MembershipList = _memberService.GetAll();
        }
    }
}