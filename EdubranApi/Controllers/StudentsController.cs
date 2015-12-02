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
    /// This controller handles all the requests associated with the student models
    /// </summary>
    [RoutePrefix("api/Students")]
    [Authorize]
    public class StudentsController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        // GET: api/Students
        /// <summary>
        /// get the list of all students
        /// </summary>
        /// <returns></returns>
        [Route("GetAllStudents")]
        [HttpGet]
        public IQueryable<StudentDTO> GetAllStudents()
        {
            var students = from b in db.Students
                           select new StudentDTO
                           {
                               student_number= b.Id,
                               first_name = b.firstName,
                               middle_name = b.middleName,
                               last_name = b.lastName,
                               profile_pic = b.profilePic,
                               wall_paper = b.wallpaper,
                               category = b.category,
                               instituiton=b.institute,
                               level = b.level
                           };
            return students;
        }

        /// <summary>
        /// get list of all students by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Route("GetStudentsByCategory")]
        [HttpGet]
        public IQueryable<StudentDTO> GetStudentsByCategory(string category)
        {
            var students = from b in db.Students.Where(b=> b.category== category)
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
        /// get student profile by Id, tested
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetStudentProfile")]
        [HttpGet]
        [ResponseType(typeof(StudentDetailedDTO))]
        public async Task<IHttpActionResult> GetStudentProfile(int id)
        {
            Student b = await db.Students.FindAsync(id);
            if (b == null)
            {
                return NotFound();
            }
            StudentDetailedDTO student = new StudentDetailedDTO()
            {
                student_number = b.Id,
                first_name = b.firstName,
                middle_name = b.middleName,
                last_name = b.lastName,
                profile_pic = b.profilePic,
                wall_paper = b.wallpaper,
                category = b.category,
                instituiton = b.institute,
                level = b.level,
                curriculum_vitae = b.cv,
                linkdn_url = b.linkdn,
                email_address = b.email,
                phone_number = b.phone,
                transcripts = b.transcripts,
                application_num= b.application_number

            };
            return Ok(student);
        }


        
        /// <summary>
        /// get current student profile, tested
        /// </summary>
        /// 
        /// <returns></returns>
        [ResponseType(typeof(StudentDetailedDTO))]
        [Route("GetCurrentStudentProfile")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCurrentStudentProfile( )
        {
            string reg = User.Identity.GetUserId();
            StudentDetailedDTO student = null;

            Student b = await db.Students.Where(d=> d.registrationId==reg).SingleOrDefaultAsync();
            if (b == null)
            {
                return NotFound();
            }
            else
            {
                student = new StudentDetailedDTO()
                {
                    student_number = b.Id,
                    first_name = b.firstName,
                    middle_name = b.middleName,
                    last_name = b.lastName,
                    profile_pic = b.profilePic,
                    wall_paper = b.wallpaper,
                    category = b.category,
                    instituiton = b.institute,
                    level = b.level,
                    curriculum_vitae = b.cv,
                    linkdn_url = b.linkdn,
                    email_address = b.email,
                    phone_number = b.phone,
                    transcripts = b.transcripts,
                    application_num = b.application_number

                };
            }

            return Ok(student);
        }


        
        /// <summary>
        /// Edit current student profile, tested
        /// </summary>
        /// 
        /// <param name="profile"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("EditStudentProfile")]
        [HttpPut]
        public async Task<IHttpActionResult> EditStudentProfile(StudentEditDTO profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string reg = User.Identity.GetUserId();

            Student client = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
            if (client == null)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }



            Student student = new Student()
            {
                firstName= profile.first_name,
                middleName = profile.middle_name,
                lastName = profile.last_name,
                level = profile.level,
                category= profile.category,
                cv = profile.curriculum_vitae,
                email = profile.email_address,
                institute= profile.instituiton,
                transcripts = profile.profile_pic,
                phone = profile.phone_number,
                wallpaper = profile.wall_paper,
                linkdn = profile.profile_pic,
                profilePic = profile.profile_pic,
                Id = client.Id
            };

            foreach (PropertyInfo propertyInfo in client.GetType().GetProperties())
            {
                if (propertyInfo.GetValue(student, null) == null)
                    propertyInfo.SetValue(student, propertyInfo.GetValue(client, null), null);
            }
            

            try
            {
                db.Entry(client).CurrentValues.SetValues(student);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(client.Id))
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

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.Id == id) > 0;
        }
    }
}