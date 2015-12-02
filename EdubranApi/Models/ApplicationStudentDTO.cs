using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class ApplicationStudentDTO
    {
        /// <summary>
        /// Application id
        /// </summary>
        public int application_num { get; set; }

        /// <summary>
        /// Student motivation for applying this project
        /// </summary>
        public string motivation { get; set; }

        /// <summary>
        /// -1 failed, 0 pending, 1 successfull
        /// </summary>
        public int applicationStatus { get; set; }

        /// <summary>
        /// company which owns the project
        /// </summary>
        public CompanyDTO company { get; set; }

        /// <summary>
        /// Projet which has been applied
        /// </summary>
        public ProjectAppDTO project { get; set; }

    }
}