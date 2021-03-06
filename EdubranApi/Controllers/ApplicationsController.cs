﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EdubranApi.Models;
using Microsoft.AspNet.Identity;

namespace EdubranApi.Controllers
{
    /// <summary>
    /// The controller that deals with all endpoints for application processing
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Applications")]
    public class ApplicationsController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        // GET: api/Applications
        /// <summary>
        /// This retrieves all the applications in the system, its only useful for debugging purposes and maybe administrative purposes
        /// </summary>
        /// <returns></returns>
        [Route("GetAllApplications")]
        [HttpGet]
        public IQueryable<ApplicationDTO> GetApplications()
        {
            var applications = from b in db.Applications
                               .Include(b => b.company)
                               .Include(b => b.student)
                               .Include(b => b.project)

                               select new ApplicationDTO
                               {
                                   application_num = b.Id,
                                   applicationStatus = b.applicationStatus,
                                   motivation = b.motivation,
                                   student = new StudentDTO {
                                                                student_number = b.student.Id,
                                                                first_name = b.student.firstName,
                                                                middle_name = b.student.middleName,
                                                                last_name = b.student.lastName,
                                                                wall_paper = b.student.wallpaper,
                                                                profile_pic = b.student.profilePic,
                                                                category = b.student.category,
                                                                level = b.student.level,
                                                                instituiton = b.student.institute
                                                                
                                                            },
                                   company = new CompanyDTO{
                                                                companyID = b.company.Id,
                                                                name = b.company.companyName,
                                                                company_category= b.company.category,
                                                                profile_pic = b.company.profilePicture,
                                                                wall_pic = b.company.wallpaper
                                                           },
                                   project = new ProjectAppDTO{
                                                                project_id = b.project.Id,
                                                                project_category= b.project.category,
                                                                project_title = b.project.title,
                                                                project_status = b.project.status,
                                                                targeted_level = b.project.audience
                                                                
                                                            }

                               };
            return applications;
        }

        /// <summary>
        /// Retrieves all applications made by the student currently logged in
        /// </summary>
        /// <returns></returns>
        [Route("GetCurrentStudentApplications")]
        [HttpGet]
        public IQueryable<ApplicationStudentDTO> GetStudentApplications()
        {
            string reg = User.Identity.GetUserId();
            Student client =  db.Students.Where(d => d.registrationId == reg).SingleOrDefault();
            if (client == null)
            {
                return null;
            }
            var applications = from b in db.Applications
                               .Include(b => b.company)
                               .Include(b => b.project)
                               .Where(b => b.studentId ==client.Id)

                               select new ApplicationStudentDTO
                               {
                                   application_num = b.Id,
                                   applicationStatus = b.applicationStatus,
                                   motivation = b.motivation,
                                   company = new CompanyDTO
                                   {
                                       companyID = b.company.Id,
                                       name = b.company.companyName,
                                       company_category = b.company.category,
                                       profile_pic = b.company.profilePicture,
                                       wall_pic = b.company.wallpaper
                                   },
                                   project = new ProjectAppDTO
                                   {
                                       project_id = b.project.Id,
                                       project_category = b.project.category,
                                       project_title = b.project.title,
                                       project_status = b.project.status,
                                       targeted_level = b.project.audience

                                   }

                               };
            return applications;
        }



        /// <summary>
        /// Retrieves all applications made by the student currently logged in
        /// </summary>
        /// <returns></returns>
        [Route("GetProjectApplications")]
        [HttpGet]
        public IQueryable<ApplicationDTO> GetProjectApplications( int project_id)
        {
            string reg = User.Identity.GetUserId();
            Project project = db.Projects.Find(project_id);
            if (project == null)
            {
                return null;
            }
            var applications = from b in db.Applications
                               .Include(b => b.company)
                               .Include(b => b.project)
                               .Include(b => b.student)
                               .Where(b => b.projectId== project_id)

                               select new ApplicationDTO
                               {
                                   application_num = b.Id,
                                   applicationStatus = b.applicationStatus,
                                   motivation = b.motivation,
                                   company = new CompanyDTO
                                   {
                                       companyID = b.company.Id,
                                       name = b.company.companyName,
                                       company_category = b.company.category,
                                       profile_pic = b.company.profilePicture,
                                       wall_pic = b.company.wallpaper
                                   },
                                   project = new ProjectAppDTO
                                   {
                                       project_id = b.project.Id,
                                       project_category = b.project.category,
                                       project_title = b.project.title,
                                       project_status = b.project.status,
                                       targeted_level = b.project.audience

                                   },
                                   student = new StudentDTO
                                   {
                                       student_number = b.student.Id,
                                       first_name = b.student.firstName,
                                       middle_name = b.student.middleName,
                                       last_name = b.student.lastName,
                                       wall_paper = b.student.wallpaper,
                                       profile_pic = b.student.profilePic,
                                       category = b.student.category,
                                       level = b.student.level,
                                       instituiton = b.student.institute

                                   }

                               };
            return applications;
        }



        /// <summary>
        /// Retrieves the applications associated with the currently logged in company
        /// </summary>
        /// <returns></returns>
        [Route("GetCurrentCompanyApplications")]
        [HttpGet]
        public IQueryable<ApplicationStudentDTO> GetCompanyApplications()
        {
            string reg = User.Identity.GetUserId();
            Company client = db.Companies.Where(d => d.registrationId == reg).SingleOrDefault();
            if (client == null)
            {
                return null;
            }
            var applications = from b in db.Applications
                               .Include(b => b.project)
                               .Include(b=> b.student)
                               .Where(b => b.companyId == client.Id)

                               select new ApplicationStudentDTO
                               {
                                   application_num = b.Id,
                                   applicationStatus = b.applicationStatus,
                                   motivation = b.motivation,
                                   company = new CompanyDTO
                                   {
                                       companyID = b.company.Id,
                                       name = b.company.companyName,
                                       company_category = b.company.category,
                                       profile_pic = b.company.profilePicture,
                                       wall_pic = b.company.wallpaper
                                   },
                                   project = new ProjectAppDTO
                                   {
                                       project_id = b.project.Id,
                                       project_category = b.project.category,
                                       project_title = b.project.title,
                                       project_status = b.project.status,
                                       project_pic = b.project.post_pic,
                                       targeted_level = b.project.audience

                                   }

                               };
            return applications;
        }


        /// <summary>
        /// Retrives an application given its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 on success</returns>
        [ResponseType(typeof(ApplicationDTO))]
        [Route("GetApplication")]
        [HttpGet]
        public async Task<IHttpActionResult> GetApplication(int id)
        {
            Application b = await db.Applications.FindAsync(id);
            if (b == null)
            {
                return NotFound();
            }
            ApplicationDTO application = new ApplicationDTO
            {
                application_num = b.Id,
                applicationStatus = b.applicationStatus,
                motivation = b.motivation,
                student = new StudentDTO
                {
                    student_number = b.student.Id,
                    first_name = b.student.firstName,
                    middle_name = b.student.middleName,
                    last_name = b.student.lastName,
                    wall_paper = b.student.wallpaper,
                    profile_pic = b.student.profilePic,
                    category = b.student.category,
                    level = b.student.level,
                    instituiton = b.student.institute

                },
                company = new CompanyDTO
                {
                    companyID = b.company.Id,
                    name = b.company.companyName,
                    company_category = b.company.category,
                    profile_pic = b.company.profilePicture,
                    wall_pic = b.company.wallpaper
                },
                project = new ProjectAppDTO
                {
                    project_id = b.project.Id,
                    project_category = b.project.category,
                    project_title = b.project.title,
                    project_status = b.project.status,
                    project_pic = b.project.post_pic,
                    targeted_level = b.project.audience

                }
            };

            return Ok(application);
        }
        
        /// <summary>
        /// This is used by students to create applications, 1 application is allowed per project
        /// </summary>
        /// <param name="app_data"></param>
        /// <returns>200 on success, 406 on failure if student doesn't qualify</returns>
        [Route("CreateApplication")]
        [HttpPost]
        public async Task<IHttpActionResult> PostApplication(ApplicationPostDTO app_data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string reg = User.Identity.GetUserId();
            Student client = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
            if (client == null)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }

            

            Project project = await db.Projects.FindAsync(app_data.projectId);
            if (project == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            int check =  db.Applications.Count(d => d.projectId == app_data.projectId && d.studentId == client.Id);

            if (check > 0)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            // check if student information matches minimum requirements for the project
            if (project.category!=client.category || client.level<project.audience)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);

            }
           
            Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            if(timeStamp> project.closingDate)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            Application application = new Application
            {
                motivation = app_data.motivation,
                companyId =  project.companyId,
                studentId =  client.Id,
                projectId = app_data.projectId,
                applicationStatus = 0,
                applicationDate =""+ DateTime.UtcNow.Month + "/"+DateTime.UtcNow.Day + "/"+DateTime.UtcNow.Year,
                applicationTime = timeStamp
            };

            Application b = db.Applications.Add(application);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        /// <summary>
        /// it checks whether a student has applied for that project, returns 1 if applied, and -1 when not applied otherwise 0
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("CheckApplication")]
        [HttpGet]
        public int checkApplication(int id)
        {
            string reg = User.Identity.GetUserId();
            Student client =  db.Students.Where(d => d.registrationId == reg).SingleOrDefault();
            if (client == null)
            {
                return 0;
            }

            Project project =  db.Projects.Find(id);
            if (project == null)
            {
                return 0;
            }


            int check = db.Applications.Count(d => d.projectId == id && d.studentId == client.Id);

            if (check > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationExists(int id)
        {
            return db.Applications.Count(e => e.Id == id) > 0;
        }
    }
}