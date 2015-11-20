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
       
        [Route("getSkills")]
        [HttpGet]
        public IQueryable<Skill> GetSkills(int student_id)
        {
            var skills = from b in db.Skills.Where(b => b.studentId == student_id)
                         select new Skill
                         {
                            Id= b.Id,
                            skill= b.skill,
                            studentId= b.studentId
                         };
                         
            return skills;
        }
        /// <summary>
        /// Returns skills for the current student
        /// </summary>
        /// <returns></returns>
        [Route("getSkills")]
        [HttpGet]
        public IQueryable<Skill> GetSkills()
        {
            string reg = User.Identity.GetUserId();
            Student student = db.Students.First(b => b.registrationId == reg);
            if (student == null)
            {
                return null;
            }
            var skills = from b in db.Skills.Where(b => b.studentId == student.Id)
                         select new Skill
                         {
                             Id = b.Id,
                             skill = b.skill,
                             studentId = b.studentId
                         };

            return skills;
        }

        // GET: api/Skills/5
        /// <summary>
        /// Returns the skill associated with that skill id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Skill))]
        [Route("getSkill")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSkill(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // PUT: api/Skills/5
        [ResponseType(typeof(void))]
        [Route("EditSkill")]
        [HttpPut]
        public async Task<IHttpActionResult> PutSkill(int id, string skill_name)
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

            if (!SkillExists(id))
            {
                return BadRequest();
            }
            Skill skill=  new Skill()
            {
                skill = skill_name,
                studentId = student.Id
            };
            db.Entry(skill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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

        // POST: api/Skills
        [ResponseType(typeof(void))]
        [Route("AddSkill")]
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
            for(int i =0; i<skill_names.Length; i++)
            {
                skills.Add(new Skill()
                {
                    skill = skill_names[i],
                    studentId = student_id
                });
            }
            
            db.Skills.AddRange(skills);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/Skills/5
        [ResponseType(typeof(void))]
        [Route("DeleteSkill")]
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