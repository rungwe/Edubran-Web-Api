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
    /// This controller facilitates student skills, you can see skills of a students, edit delete etc
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Skill")]
    public class SkillsController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        // GET: api/Skills
        /// <summary>
        /// returns all the skills associated with a student given the student id
        /// </summary>
        /// <param name="student_id"></param>
        /// <returns></returns>
       
        [Route("getStudentSkills")]
        [HttpGet]
        public IQueryable<SkillDTO> GetSkills(int student_id)
        {
            var skills = from b in db.Skills.Where(b => b.studentId == student_id)
                         select new SkillDTO
                         {
                             skill_id = b.Id,
                             skill_name = b.skill
                         };

            return skills;
        }
        /// <summary>
        /// Returns skills for the current student logged in, tested
        /// </summary>
        /// <returns></returns>
        [Route("getCurrentStudentSkills")]
        [HttpGet]
        public IQueryable<SkillDTO> GetSkills()
        {
            string reg = User.Identity.GetUserId();
            Student student = db.Students.Where(d => d.registrationId == reg).SingleOrDefault();
            if (student == null)
            {
                return null;
            }
            var skills = from b in db.Skills.Where(b => b.studentId == student.Id)
                         select new SkillDTO
                         {
                             skill_id= b.Id,
                             skill_name= b.skill
                         };

            return skills;
        }

        
        /// <summary>
        /// Returns the skill associated with that skill id, it could be useful for debugging
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(SkillDTO))]
        [Route("getSkill")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSkill(int id)
        {
            Skill b = await db.Skills.FindAsync(id);
            if (b == null)
            {
                return NotFound();
            }
            SkillDTO skill = new SkillDTO
            {
                skill_id = b.Id,
                skill_name = b.skill
            };
            return Ok(skill);
        }

        /// <summary>
        /// Edit skill of the currently logged in student, it accepts the the id of the skill and the new name of the skill, tested
        /// </summary>
        /// <param name="skill_data"></param>
        /// <returns>200 on success</returns>
        [ResponseType(typeof(void))]
        [Route("EditSkill")]
        [HttpPut]
        public async Task<IHttpActionResult> PutSkill(SkillDTO skill_data)
        {
            string reg = User.Identity.GetUserId();
            Student student = await db.Students.FirstAsync(b => b.registrationId == reg);
            if (student == null)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!SkillExists(skill_data.skill_id))
            {
                return BadRequest();
            }
            Skill skill=  new Skill()
            {
                skill = skill_data.skill_name,
                studentId = student.Id,
                Id = skill_data.skill_id
            };
            db.Entry(skill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(skill_data.skill_id))
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
        /// Create skills for the currently logged in student, if skill already exists it is simply skipped, tested
        /// </summary>
        /// <param name="skill_names"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("AddSkills")]
        [HttpPost]
        public async Task<IHttpActionResult> PostSkill(string[] skill_names)
        {
            string reg = User.Identity.GetUserId();
            Student student = await db.Students.FirstAsync(b => b.registrationId == reg);
            if (student == null)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            int student_id = student.Id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Skill> skills = new List<Skill>();
         
            foreach (string skill_name in skill_names)
            {
                int check = await db.Skills.CountAsync(d => d.skill == skill_name && d.studentId == student_id);
                if (check > 0)
                {
                    continue;
                }
                skills.Add(new Skill()
                {
                    skill = skill_name,
                    studentId = student_id
                });
            }
            
            IEnumerable<Skill> new_skills = db.Skills.AddRange(skills);
            await db.SaveChangesAsync();
            List<SkillDTO> processed_skill = new List<SkillDTO>();
            foreach( Skill sk in new_skills)
            {
                processed_skill.Add(new SkillDTO()
                {
                    skill_id= sk.Id,
                    skill_name = sk.skill
                });
            }
            return Ok(processed_skill);
        }

        /// <summary>
        /// remove skill of the particular logged in student, tested 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("RemoveSkill")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteSkill(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            db.Skills.Remove(skill);
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

        private bool SkillExists(int id)
        {
            return db.Skills.Count(e => e.Id == id) > 0;
        }
    }
}