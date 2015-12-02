using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    /// <summary>
    /// Proect Data transfer for applications
    /// </summary>
    public class ProjectAppDTO
    {
        public int project_id { get; set; }
        public string project_title { get; set; }
        public string project_status { get; set; }
        public string project_category { get; set; }
        /// <summary>
        /// The targeted level eg first year = 1, second year = 2, third year=3, fourth year/hounors = 4, masters= 6, phd=7
        /// </summary>
        public int targeted_level { get; set; }
      
    }
}