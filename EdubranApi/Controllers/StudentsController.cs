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
    [RoutePrefix("api/Students")]
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
            var students = from b in db.Students.Include(b => b.Skills).Where(b=> b.category== category)
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


        // GET: api/Students/5
        /// <summary>
        /// get student profile by Id
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
                student_skills = b.Skills,
                transcripts = b.transcripts

            };
            return Ok(student);
        }


        // GET: api/GetCurrentStudentProfile/5
        /// <summary>
        /// get current student profile
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

            Student b = await db.Students.Include(d=> d.Skills).FirstAsync(d=> d.registrationId== reg);
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
                    student_skills = b.Skills,
                    transcripts = b.transcripts

                };
            }

            return Ok(student);
        }


        
        /// <summary>
        /// Edit student profile
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
                profilePic = profile.profile_pic

            };
            string reg = User.Identity.GetUserId();
            

            Student client = await db.Students.FirstAsync(b => b.registrationId == reg);
            student.Id = client.Id;
            
            db.Entry(student).State = EntityState.Modified;

            try
            {
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        /**
        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> DeleteStudent(int id)
        {
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            await db.SaveChangesAsync();

            return Ok(student);
        }
        **/

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