using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AceJobAgency.Model
{
    public class AceJobUser : IdentityUser
    {
  
        public string FName { get; set; }

        public string LName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "NRIC")]
        public string NRIC { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        public string? ResumeURL { get; set; }

        [DataType(DataType.MultilineText)]
        public string Whoami { get; set; }

        public string Token { get; set;  }
    }
}
