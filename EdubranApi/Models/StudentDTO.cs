using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class StudentDTO
    {
        public int student_number { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string profile_pic { get; set; }
        public string wall_paper { get; set; }
        public string category { get; set; }
        public string gender { get; set; }
        /// <summary>
        /// name of the university or techknikon
        /// </summary>
        public string instituiton { get; set; }
        /// <summary>
        /// academic level, 1 means first year, 2 means second year etc
        /// </summary>
        public int level { get; set; }

        //public ICollection<Skill> student_skills { get; set; }
    }
}