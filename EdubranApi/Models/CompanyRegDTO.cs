using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class CompanyRegDTO
    {
        [Required]
        public string company_name { get; set; }
        [Required]
        public string email_address { get; set; }
        [Required]
        public string company_category { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string confirm_password { set; get; }
    }
}