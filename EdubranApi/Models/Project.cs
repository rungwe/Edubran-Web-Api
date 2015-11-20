using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    /// <summary>
    /// Project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// project Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string detailsText { get; set; }
        public string detailsResourceUrl { get; set; }
        public string launchDate { get; set; }
        public string dueDate { get; set; }
        /// <summary>
        /// The targeted audience eg first year = 1, second year = 2 ....
        /// </summary>
        public int audience { get; set; }
        public int numViews { get; set; }
        public int numComments { get; set; }
        public int numApplication { get; set; }

        // unimplemented fields
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        
        public int value1 { get; set; }
        public int value2 { get; set; }
        public int value3 { get; set; }


        // Foreign key
        public int companyId { get; set; }
        // Navigation property
        public Company company { get; set; }


    }
}