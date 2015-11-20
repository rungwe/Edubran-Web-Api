using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    /// <summary>
    /// Student
    /// </summary>
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string registrationId { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public int phone { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string profilePic { get; set; }
        public string wallpaper { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        /// <summary>
        /// academic level, 1 means first year, 2 means second year etc
        /// </summary>
        public int level { get; set; }
        public string transcripts { get; set; }
        public string cv { get; set; }
        public string linkdn { get; set; }
        [Required]
        public string institute { get; set; }

        public ICollection<Skill> Skills { get; set; }


    }
}