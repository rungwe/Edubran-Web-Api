using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class CompanyEditDTO
    {
        public string name { get; set; }
        public string wall_pic { get; set; }
        public string profile_pic { get; set; }
        public string company_category { get; set; }
        public string email_address { get; set; }
        public string statusMessage { get; set; }
        public string web_url { get; set; }
        public string physical_address { get; set; }
        public string telephone { get; set; }
        public string fax_num { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string linkdn { get; set; }
        public string google_plus { get; set; }
    }
}