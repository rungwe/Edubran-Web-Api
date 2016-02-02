using EdubranApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EdubranApi.Controllers
{
    /// <summary>
    /// This controller handles all search queries
    /// </summary>
    [RoutePrefix("api/Search")]
    [Authorize]
    public class SearchController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        // GET: api/Students
        /// <summary>
        /// get the list of all students
        /// </summary>
        /// <returns></returns>
        [Route("Students")]
        [HttpGet]
        public IQueryable<StudentDTO> searchStudents(string searchQuery)
        {
            if( searchQuery=="" || searchQuery == null) {

                return null;
            }

            var students = from b in db.Students.Where( b=> b.firstName.Contains(searchQuery) || b.middleName.Contains(searchQuery) || b.lastName.Contains(searchQuery)).OrderBy(b=> b.firstName)
                           select new StudentDTO
                           {
                               student_number = b.Id,
                               first_name = b.firstName,
                               middle_name = b.middleName,
                               last_name = b.lastName,
                               profile_pic = b.profilePic,
                               wall_paper = b.wallpaper,
                               category = b.category,
                               instituiton = b.institute,
                               level = b.level
                           };
            return students;
        }

        /// <summary>
        /// retrieves all the companies in the system
        /// </summary>
        /// <returns></returns>
        [Route("Companies")]
        [HttpGet]
        public IQueryable<CompanyDTO> searchCompanies(string searchQuery)
        {
            var companies = from b in db.Companies.Where(b=> b.companyName.Contains(searchQuery)).OrderBy(b=> b.companyName)
                            select new CompanyDTO
                            {
                                companyID = b.Id,
                                name = b.companyName,
                                company_category = b.category,
                                profile_pic = b.profilePicture,
                                wall_pic = b.profilePicture
                            };
            return companies;
        }
    }
}
