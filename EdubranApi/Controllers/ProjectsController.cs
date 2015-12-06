using System;
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
using System.Reflection;

namespace EdubranApi.Controllers
{
    /// <summary>
    /// This controller manages projects, contains all the url endpoints which deals with projects
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Projects")]
   
    public class ProjectsController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();
        // needs pagination
        /// <summary>
        /// get all the projects from all companies and categories, tested
        /// </summary>
        /// <returns></returns>
        // GET: api/Projects
        [Route("GetAllProjects")]
        [HttpGet]
        public IQueryable<ProjectDTO> GetAllProjects()
        {
            var project = from b in db.Projects.Include(b => b.company)
                          select new ProjectDTO
                          {
                              project_id = b.Id,
                              project_title = b.title,
                              project_status = b.status,
                              project_pic = b.post_pic,
                              project_category = b.category,
                              targeted_level = b.audience,
                              num_application = b.numApplication,
                              num_comments = b.numComments,
                              num_views = b.numViews,
                              company = new CompanyDTO()
                              {
                                  companyID = b.company.Id,
                                  name = b.company.companyName,
                                  company_category = b.company.category,
                                  profile_pic = b.company.profilePicture,
                                  wall_pic = b.company.wallpaper
                              }
                          
                          };

            return project;


        }
        /// <summary>
        /// Retrieves the suggested project for the current logged in student, based on their category and academic level, tested
        /// </summary>
        /// <returns>returns 200 on success and null on failure</returns>
        [Route("GetSuggestedProjects")]
        [HttpGet]
        public IQueryable<ProjectDTO> GetSuggestedProjects()
        {

            string reg = User.Identity.GetUserId();

            Student client = db.Students.Where(d => d.registrationId == reg).SingleOrDefault();
            if (client == null)
            {
                return null;
            }
            var project = from b in db.Projects.Include(b => b.company).Where(b => b.category == client.category && b.audience == client.level)
                          select new ProjectDTO
                          {
                              project_id = b.Id,
                              project_title = b.title,
                              project_status = b.status,
                              project_pic = b.post_pic,
                              project_category = b.category,
                              targeted_level = b.audience,
                              num_application = b.numApplication,
                              num_comments = b.numComments,
                              num_views = b.numViews,
                              company = new CompanyDTO()
                              {
                                  companyID = b.company.Id,
                                  name = b.company.companyName,
                                  company_category = b.company.category,
                                  profile_pic = b.company.profilePicture,
                                  wall_pic = b.company.wallpaper
                              }

                          };

            return project;


        }

        /// <summary>
        /// get projects posted by a company by supplying the company id, if the company_id is not found returns null
        /// </summary>
        /// <param name="company_id"></param>
        /// <returns></returns>
        [Route("GetCompanyProjects")]
        [HttpGet]
        public IQueryable<ProjectDTO> GetCompanyProjects(int company_id)
        {
            Company company = db.Companies.Find(company_id);
            if (company == null)
            {
                return null;
            }
            var project = from b in db.Projects.Include(b => b.company).Where(b=> b.companyId==company_id)
                          select new ProjectDTO
                          {
                              project_id = b.Id,
                              project_title = b.title,
                              project_status = b.status,
                              project_pic = b.post_pic,
                              project_category = b.category,
                              targeted_level = b.audience,
                              num_application = b.numApplication,
                              num_comments = b.numComments,
                              num_views = b.numViews,
                              company = new CompanyDTO()
                              {
                                  companyID = b.company.Id,
                                  name = b.company.companyName,
                                  company_category = b.company.category,
                                  profile_pic = b.company.profilePicture,
                                  wall_pic = b.company.wallpaper
                              }

                          };

            return project;


        }


        /// <summary>
        /// get projects posted by the current company logged in
        /// </summary>
        /// <returns></returns>
        [Route("GetCurrentCompanyProjects")]
        [HttpGet]
        public IQueryable<ProjectDTO> GetCurrentCompanyProjects()
        {
            string reg = User.Identity.GetUserId();

            Company company = db.Companies.Where(d => d.registrationId == reg).SingleOrDefault(); 
            if (company == null)
            {
                return null;
            }
            int company_id = company.Id;
            var project = from b in db.Projects.Include(b => b.company).Where(b => b.companyId == company_id)
                          select new ProjectDTO
                          {
                              project_id = b.Id,
                              project_title = b.title,
                              project_status = b.status,
                              project_pic = b.post_pic,
                              project_category = b.category,
                              targeted_level = b.audience,
                              num_application = b.numApplication,
                              num_comments = b.numComments,
                              num_views = b.numViews,
                              company = new CompanyDTO()
                              {
                                  companyID = b.company.Id,
                                  name = b.company.companyName,
                                  company_category = b.company.category,
                                  profile_pic = b.company.profilePicture,
                                  wall_pic = b.company.wallpaper
                              }

                          };

            return project;


        }


        /// <summary>
        /// get projects by category, eg "Engineering", "Commerce", "Health", "Humanities", "Law", "Science"
        /// </summary>
        /// <param name="category"></param>
        /// <returns>projects from a certain category</returns>
        [Route("GetProjectsByCategory")]
        [HttpGet]
        public IQueryable<ProjectDTO> GetProjectByCategory(string category)
        {
            var project = from b in db.Projects.Include(b => b.company).Where(b => b.category == category)
                          select new ProjectDTO
                          {
                              project_id = b.Id,
                              project_title = b.title,
                              project_status = b.status,
                              project_pic = b.post_pic,
                              project_category = b.category,
                              targeted_level = b.audience,
                              num_application = b.numApplication,
                              num_comments = b.numComments,
                              num_views = b.numViews,
                              company = new CompanyDTO()
                              {
                                  companyID = b.company.Id,
                                  name = b.company.companyName,
                                  company_category = b.company.category,
                                  profile_pic = b.company.profilePicture,
                                  wall_pic = b.company.wallpaper
                              }
                          };
            return project;


        }


        /// <summary>
        /// Get project details by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [ResponseType(typeof(ProjectDetailDTO))]
        [Route("GetProjectDetail")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProjectDetail(int id)
        {
            var project = await db.Projects.Include(b => b.company).Select(b =>
        new ProjectDetailDTO()
        {
            project_id = b.Id,
            project_title = b.title,
            project_pic = b.post_pic,
            project_category = b.category,
            project_status = b.status,
            description = b.detailsText,
            attachment = b.detailsResourceUrl,
            launch_date = b.launchDate,
            due_date = b.dueDate,
            targeted_level = b.audience,
            num_views = b.numViews,
            num_application = b.numApplication,
            num_comments = b.numComments,
            company = new CompanyDTO()
            {
                companyID = b.company.Id,
                name = b.company.companyName,
                company_category = b.company.category,
                profile_pic = b.company.profilePicture,
                wall_pic = b.company.wallpaper
            }

        }).SingleOrDefaultAsync(b => b.project_id == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        /// <summary>
        /// Edit a project given the project id and neccessary details to be edited
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project_data"></param>
        /// <returns>http code 200 on success, 403 when there is a privacy violation</returns>
        
        [ResponseType(typeof(void))]
        [Route("EditProject")]
        [HttpPut]
        public async Task<IHttpActionResult> EditProject(int id, ProjectEditDTO project_data)
        {
            string reg = User.Identity.GetUserId();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProjectExists(id))
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            Company company = await db.Companies.FirstAsync(b => b.registrationId == reg);
            int company_id = company.Id;

            Project project = new Project()
            {
                Id = id,
                companyId = company_id,
                title = project_data.project_title,
                detailsText = project_data.description,
                detailsResourceUrl = project_data.attachment,
                audience = project_data.targeted_level,
                category = project_data.project_category,
                status = "open",
                dueDate = project_data.due_date
            };

            Project original_project = await db.Projects.FindAsync(id);
            if (original_project.company.Id != company_id)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            foreach (PropertyInfo propertyInfo in original_project.GetType().GetProperties())
            {
                if (propertyInfo.GetValue(project, null) == null)
                    propertyInfo.SetValue(project, propertyInfo.GetValue(original_project, null), null);
            }

            try
            {
                db.Entry(original_project).CurrentValues.SetValues(project);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        /// <summary>
        /// a method used by companies to post projects
        /// </summary>
        /// <param name="project_data"></param>
        /// <returns>200 on success</returns>
        [ResponseType(typeof(ProjectDetailDTO))]
        [Route("CreateProject")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateProject(ProjectPostDTO project_data)
        {

            string reg = User.Identity.GetUserId();
            Company owner = await db.Companies.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

            if (owner == null)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            int owner_id = owner.Id;
            if (owner==null)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            Project project = new Project()
            {

                title = project_data.project_title,
                detailsText = project_data.description,
                detailsResourceUrl = project_data.attachment,
                audience = project_data.targeted_level,
                category = project_data.project_category,
                companyId = owner_id,
                dueDate = project_data.due_date

            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Project posted = db.Projects.Add(project);
            await db.SaveChangesAsync();

            ProjectDetailDTO posted_project = new ProjectDetailDTO() {
                project_id = posted.Id,
                project_title = posted.title,
                project_category = posted.category,
                project_status = posted.status,
                description = posted.detailsText,
                attachment = posted.detailsResourceUrl,
                launch_date = posted.launchDate,
                due_date = posted.dueDate,
                targeted_level = posted.audience,
                num_views = posted.numViews,
                num_application = posted.numApplication,
                num_comments = posted.numComments,
                company = new CompanyDTO()
                {
                    companyID = posted.company.Id,
                    name = posted.company.companyName,
                    company_category = posted.company.category,
                    profile_pic = posted.company.profilePicture,
                    wall_pic = posted.company.wallpaper
                }

            };
            return Ok(posted_project);
           // return StatusCode(HttpStatusCode.Created);
        }


        // DELETE: api/Projects/5
        /// <summary>
        /// Deletes a project given its id, only the company that created the project is able to perfom this operation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Project))]
        [Route("DeleteProjects")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProject(int id)
        {
            string reg = User.Identity.GetUserId();
            Company owner = await db.Companies.FirstAsync(b => b.registrationId == reg);
            int owner_id = owner.Id;

            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            if (project.companyId!= owner_id)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.Id == id) > 0;
        }
    }
}