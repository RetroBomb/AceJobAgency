using AceJobAgency.Model;
using AceJobAgency.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using System.Dynamic;

namespace AceJobAgency.Pages.Member
{
    [Authorize(Roles = "Member")]
    public class MemberIndexModel : PageModel
    {
        public MembershipService _memberService { get; set; }


        public MemberIndexModel(MembershipService memberService)
        {
            _memberService = memberService;
        }

        public void onGet()
        {

        }
    }
}
