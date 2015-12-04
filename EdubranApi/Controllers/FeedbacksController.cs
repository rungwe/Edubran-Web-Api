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

namespace EdubranApi.Controllers
{
    /// <summary>
    /// this controller deals with comments
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Comments")]
    public class FeedbacksController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        // GET: api/Feedbacks
        /// <summary>
        /// Retrieves comments for a particular project post, tested
        /// </summary>
        /// <param name="project_id"></param>
        /// <returns></returns>
        [Route("GetComments")]
        [HttpGet]
        public IQueryable<CommentDTO> GetFeedbacks(int project_id)
        {
            var comments = from b in db.Feedbacks.Where(b => b.projectId == project_id)
              
                           select new CommentDTO
                           {
                               Id = b.Id,
                               comment = b.comment,
                               date = b.date,
                               client = new ClientDTO()
                               {
                                   clientId = b.clientId,
                                   name = b.name,
                                   profile_picture = b.profile_picture,
                                   type = b.type
                               }
                           };
            return comments;
        }

        // GET: api/Feedbacks/5
        /// <summary>
        /// Edit comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Feedback))]
        [ApiExplorerSettings(IgnoreApi =true)]
        public async Task<IHttpActionResult> GetFeedback(int id)
        {
            Feedback feedback = await db.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        // PUT: api/Feedbacks/5
        [ResponseType(typeof(void))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IHttpActionResult> PutFeedback(int id, Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedback.Id)
            {
                return BadRequest();
            }

            db.Entry(feedback).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        /// <summary>
        /// Post comments to projects for both students and companies, tested
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [ResponseType(typeof(CommentPostDTO))]
        [Route("PostComment")]
        [HttpPost]
        public async Task<IHttpActionResult> PostFeedback(CommentPostDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Project project = await db.Projects.FindAsync(comment.project_id);
            if (project == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            string reg = User.Identity.GetUserId();
            
            Student student = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
            Company company = await db.Companies.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
            Feedback b;
            if (student != null)
            {
                string middle_name = student.middleName;
                if (middle_name == null)
                {
                    middle_name="";
                }
                Feedback feedback = new Feedback()
                {
                    projectId = comment.project_id,
                    comment = comment.comment,
                    clientId = student.Id,
                    type= "student",
                    date = "today",
                    name = student.firstName +" "+middle_name+" "+ student.lastName,
                    profile_picture = student.profilePic
                    
                };
                b= db.Feedbacks.Add(feedback);
                await db.SaveChangesAsync();
                CommentDTO new_comment = new CommentDTO
                {
                    Id = b.Id,
                    comment = b.comment,
                    date = b.date,
                    client = new ClientDTO()
                    {
                        clientId = b.clientId,
                        name = b.name,
                        profile_picture = b.profile_picture,
                        type = b.type
                    }
                };
                return Ok(new_comment);
            }

            else if (company != null)
            {
                Feedback feedback = new Feedback()
                {
                    projectId = comment.project_id,
                    comment = comment.comment,
                    clientId = company.Id,
                    name = company.companyName,
                    profile_picture = company.profilePicture,
                    type = "company",
                    date = "today",
                    
                };
                b = db.Feedbacks.Add(feedback);
                await db.SaveChangesAsync();
                CommentDTO new_comment = new CommentDTO
                {
                    Id = b.Id,
                    comment = b.comment,
                    date = b.date,
                    client = new ClientDTO()
                    {
                        clientId = b.clientId,
                        name = b.name,
                        profile_picture = b.profile_picture,
                        type = b.type
                    }
                };
                return Ok(new_comment);
            }

            else
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }

            
        }

        /// <summary>
        /// Remove a comment given the comment Id, only the owner of the comment is allowed to remove the comment, tested
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Feedback))]
        [Route("RemoveComment")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFeedback(int id)
        {
            string reg = User.Identity.GetUserId();
            Student student = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
            Company company = await db.Companies.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

            Feedback feedback = await db.Feedbacks.FindAsync(id);
            if (feedback == null )
            {
                return NotFound();
            }

            if (company!=null)
            {
                if (feedback.clientId != company.Id)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                
            }

            else if (student != null)
            {
                if(feedback.clientId != student.Id)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }

            db.Feedbacks.Remove(feedback);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackExists(int id)
        {
            return db.Feedbacks.Count(e => e.Id == id) > 0;
        }
    }
}