using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class ProjectEditDTO
    {
        
        public string project_title { get; set; }
        public string project_category { get; set; }
        public string description { get; set; }
        public string attachment { get; set; }
        public string due_date { get; set; }
        /// <summary>
        /// The targeted level eg first year = 1, second year = 2, third year=3, fourth year/hounors = 4, masters= 6, phd=7
        /// </summary>
        public int targeted_level { get; set; }

        /// <summary>
        /// Project salary if applicable
        /// </summary>
        public string remuneration { get; set; }

        /// <summary>
        /// project location
        /// </summary>
        public string city { get; set; }

    }
}