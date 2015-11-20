using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    /// <summary>
    /// Comments
    /// </summary>
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string registrationId { get; set; }
        public string comment { get; set; }
        public string date { get; set; }

        /* Foreign keys
        public int StudentId { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        // Navigation property
        public Student Student { get; set; }
        public Company Company { get; set; }
        public Project Project { get; set; }
        */



    }
}