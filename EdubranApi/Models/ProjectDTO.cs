﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class ProjectDTO
    {
        public int project_id { get; set; }
        public string project_title { get; set; }
        public string project_status { get; set; }
        public string project_pic { get; set; }
        public string project_category { get; set; }
        /// <summary>
        /// The targeted level eg first year = 1, second year = 2, third year=3, fourth year/hounors = 4, masters= 6, phd=7
        /// </summary>
        public int targeted_level { get; set; }
        public int num_views { get; set; }
        public int num_comments { get; set; }
        public int num_application { get; set; }
        public int time_seconds { get; set; }
        public int time_minutes { get; set; }
        public int time_hours { get; set; }
        public int time_days { get; set; }
       // place holder
        public Int32 timestamp { get; set; }
        public string posted_date { get; set; }
        public string due_date { get; set; }

        /// <summary>
        /// Project salary if applicable
        /// </summary>
        public string remuneration { get; set; }

        /// <summary>
        /// project location
        /// </summary>
        public string city { get; set; }

        public CompanyDTO company { get; set; }


        //

    }
}