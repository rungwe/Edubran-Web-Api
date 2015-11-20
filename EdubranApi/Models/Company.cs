using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    /// <summary>
    /// Company
    /// </summary>
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string registrationId { get; set; }
        [Required]
        public string companyName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string wallpaper { get; set; }
        public string profilePicture { get; set; }
        [Required]
        public string category { get; set; }
        public string statusMessage { get; set; }
        public string website { get; set; }
        public string address { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string fb_url { get; set; }
        public string twt_url { get; set; }
        public string lkdn_url { get; set; }
        public string ggle_plus { get; set; }
        public string reg_date{ get; set; }

        // unimplemented fields
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string other4 { get; set; }
        public string other5 { get; set; }
        public string other6 { get; set; }
        public int value1 { get; set; }
        public int value2 { get; set; }
        public int value3 { get; set; }
        public int value4 { get; set; }
        public int value5 { get; set; }
        public int value6 { get; set; }


    }
}