using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class ClientDTO
    {
        [Required]
        public int clientId { get; set; }

        [Required]
        public string name { get; set; }

        public string profile_picture { get; set; }

        [Required]
        public string type { get; set; }
    }
}