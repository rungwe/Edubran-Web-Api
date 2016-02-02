using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    /// <summary>
    /// Application Model
    /// </summary>
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
        public int studentId { get; set; }
        public int companyId { get; set; }

        
        public Int32 applicationTime { get; set; }

        //Navigation property
        [ForeignKey("projectId")]
        public Project project { get; set; }

        [ForeignKey("studentId")]
        public Student student { get; set; }

        [ForeignKey("companyId")]
        public Company company { get; set; }
    }
}