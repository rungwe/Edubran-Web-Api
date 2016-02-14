using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class RecoveryEmailDTO
    {
        [Required]
        [EmailAddress]
        public string username { get; set; }
    }
}