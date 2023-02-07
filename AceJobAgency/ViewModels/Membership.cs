using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AceJobAgency.ViewModels
{
    public class Membership
    {
        [Required, MaxLength(25)]
        [BindProperty]
        public string FName { get; set; }

        [Required, MaxLength(25)]
        [BindProperty]
        public string LName { get; set; }


        [Required, MaxLength(1)]
        [Display(Name = "Gender")]
        [BindProperty]
        public string Gender { get; set; }


        [Required, RegularExpression(@"^[STFG]\d{7}[A-Z]$",
        ErrorMessage = "Invalid NRIC."), MaxLength(9)]
        [Display(Name = "NRIC")]
        [BindProperty]
        public string NRIC { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Key]
        [BindProperty]
        public string Email { get; set; }

        [Required, MinLength(12, ErrorMessage = "Password must be a minimum of 12 characters")] 
        // [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])$",
        // ErrorMessage = "Password should be a combination of lowercase, uppercase, numbers and special characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }


        [Required, DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [BindProperty]
        public string DOB { get; set; } 


        [MaxLength(50)]
        public string? ResumeURL { get; set; }


        [Required, DataType(DataType.MultilineText)]
        [BindProperty]
        public string Whoami { get; set; }

        [BindProperty]
        public string Token { get; set; }

    }
}
