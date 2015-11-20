using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class StudentRegDTO
    {
        [Required]
        public string first_name { get; set; }
        public string middle_name { get; set; }
        [Required]
        [EmailAddressAttribute]
        public string email_address { get; set; }

        [Required]
        public string last_name { get; set; }
        [Required]
        public string institute { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public int academic_level { get; set; }

        [Required]
        public string password { get; set; }

        [Required]

        public string confirm_password { get; set; }

    }
}