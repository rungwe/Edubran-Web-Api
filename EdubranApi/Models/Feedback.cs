using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string comment { get; set; }
        public string date { get; set; }
        
        public Int32 time { get; set; }

        //Foreign key
        [Required]
        public int projectId { get; set; }

        [Required]
        public int clientId { get; set; }

        [Required]
        public string name { get; set; }

        public string profile_picture { get; set; }

        [Required]
        public string type { get; set; }
        



    }
}