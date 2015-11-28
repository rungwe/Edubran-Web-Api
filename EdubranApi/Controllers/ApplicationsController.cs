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

namespace EdubranApi.Controllers
{
    public class ApplicationsController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        // GET: api/Applications
        public IQueryable<Application> GetApplications()
        {
            return db.Applications;
        }

        // GET: api/Applications/5
        [ResponseType(typeof(Application))]
        public async Task<IHttpActionResult> GetApplication(int id)
        {
            Application application = await db.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // PUT: api/Applications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutApplication(int id, Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != application.Id)
            {
                return BadRequest();
            }

            db.Entry(application).State = EntityState.Modified;


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST: api/Applications
        [ResponseType(typeof(Application))]
        public async Task<IHttpActionResult> PostApplication(Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Applications.Add(application);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = application.Id }, application);
        }

        // DELETE: api/Applications/5
        [ResponseType(typeof(Application))]
        public async Task<IHttpActionResult> DeleteApplication(int id)
        {
            Application application = await db.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            db.Applications.Remove(application);
            await db.SaveChangesAsync();

            return Ok(application);
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