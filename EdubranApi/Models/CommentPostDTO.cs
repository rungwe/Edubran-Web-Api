using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class CommentPostDTO
    {
        [Required]
        public int project_id { get; set; }
        [Required]
        public string comment { get; set; }
    }
}