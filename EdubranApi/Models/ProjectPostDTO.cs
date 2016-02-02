using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class ProjectPostDTO
    {
        
        public string project_title { get; set; }
        
        public string project_category { get; set; }
        public string description { get; set; }
        public string attachment { get; set; }
        /// <summary>
        /// the following is the date format to be used dd-mm-yyy, past days will not be allowed, due date should be at least more than a day
        /// </summary>
        public string due_date { get; set; }
        /// <summary>
        /// The targeted level eg first year = 1, second year = 2, third year=3, fourth year/hounors = 4, masters= 6, phd=7
        /// </summary>
        public int targeted_level { get; set; }
       
        
    }
}