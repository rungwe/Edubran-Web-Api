using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    /// <summary>
    /// Messaging model
    /// </summary>
    public class MessageCentre
    {

        [Required]
        public int Id { get; set; }
        public bool status { get; set; }
        public Int32 timestamp { get; set; }
        [Required]
        public string message_title { get; set; }
        public string message_body { get; set; }
        public string message_prev { get; set; }
        public string message_attachent { get; set; }

        public string project_id { get; set; }

        public string sender_id { get; set; }
        public string sender_name { get; set; }
        public string sender_picture { get; set; }
        public string sender_type { get; set; }


        public string reciever_id { get; set; }
        public string reciever_name { get; set; }
        public string receiver_picture { get; set; }
        public string receiver_type { get; set; }

    }
}