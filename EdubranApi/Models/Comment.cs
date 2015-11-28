using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //Foreign key
        public int projectId { get; set; }

        [ForeignKey("projectId")]
        public Project project { get; set; }
        
    }
}