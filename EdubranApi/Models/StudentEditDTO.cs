using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class StudentEditDTO
    {
        
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string profile_pic { get; set; }
        public string wall_paper { get; set; }
        public string category { get; set; }
        public string curriculum_vitae { get; set; }
        public string transcripts { get; set; }
        public string linkdn_url { get; set; }
        public int phone_number { get; set; }
        public string email_address { get; set; }

        /// <summary>
        /// name of the university or techknikon
        /// </summary>
        public string instituiton { get; set; }
        /// <summary>
        /// academic level, 1 means first year, 2 means second year etc
        /// </summary>
        public int level { get; set; }
    }
}