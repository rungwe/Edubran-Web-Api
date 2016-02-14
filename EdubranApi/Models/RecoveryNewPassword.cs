using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class RecoveryNewPassword
    {
        [Required]
        [EmailAddress]
        public string username { get; set; }
        [Required]
        public string passwordToken { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
    }
}