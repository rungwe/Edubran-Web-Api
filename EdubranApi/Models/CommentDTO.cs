using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class CommentDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string comment { get; set; }
        [Required]
        public string date { get; set; }
        public Int32 timestamp { get; set; }
        public int time_seconds { get; set; }
        public int time_minutes { get; set; }
        public int time_hours { get; set; }
        public int time_days { get; set; }
        /// <summary>
        /// This represents information of either a company or student who have participated in a comment
        /// </summary>
        public ClientDTO client { get; set; }
        


    }
}