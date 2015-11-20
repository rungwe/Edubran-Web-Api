using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class CompanyDTO
    {
        public int companyID { get; set; }
        public string name { get; set; }
        public string wall_pic { get; set; }
        public string profile_pic { get; set; }
        public string company_category { get; set; }
    }
}