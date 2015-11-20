using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public string motivation { get; set; }
        public string applicationDate { get; set; }
        /// <summary>
        /// -1 failed, 0 pending, 1 successfull
        /// </summary>
        public int applicationStatus { get; set; }

        // Foreign key
        public int projectId { get; set; }
        public int studentID { get; set; }
        //Navigation property
        public Project project { get; set; }
        public Student student { get; set; }
    }
}