using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class Skill
    {
        
        [Key]
        public int Id { get; set; }
        public string skill { get; set; }

        
        //Foreign key
        public int studentId { get; set; }

        [ForeignKey("studentId")]
        public Student student { get; set; }




    }
}