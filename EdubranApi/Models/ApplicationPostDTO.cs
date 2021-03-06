﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{

    /// <summary>
    /// Application DTO for posting
    /// </summary>
    public class ApplicationPostDTO
    {
        [Required]
        public string motivation { get; set; }

        [Required]
        public int projectId { get; set; }
      
    }
}