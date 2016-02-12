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
    /// This controller handles all the url endpoints associated with companies
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Companies")]
    public class CompaniesController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        // tested
        
        /// <summary>
        /// retrieves all the companies in the system
        /// </summary>
        /// <returns></returns>
        [Route("GetAllCompanies")]
        [HttpGet]
        public IQueryable<CompanyDTO> GetCompanies()
        {
            var companies = from b in db.Companies
                            select new CompanyDTO {
                                companyID = b.Id,
                                name = b.companyName,
                                company_category = b.category,
                                profile_pic = b.profilePicture,
                                wall_pic = b.profilePicture
                            };
            return companies;
        }

        //tested
        /// <summary>
        /// Retrieves companies by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Route("GetCompaniesByCategory")]
        [HttpGet]
        public IQueryable<CompanyDTO> GetCompaniesByCategory(string category)
        {
            var companies = from b in db.Companies.Where(d=> d.category== category)
                            select new CompanyDTO
                            {
                                companyID = b.Id,
                                name = b.companyName,
                                company_category = b.category,
                                profile_pic = b.profilePicture,
                                wall_pic = b.wallpaper
                            };
            return companies;
        }

        // tested
        // GET: api/Companies/5
        /// <summary>
        /// Retrieves companies by their categories
        /// </summary>
        /// <returns>200non success and 404 if there are no companies</returns>
        [ResponseType(typeof(CompanyProfileDTO))]
        [Route("GetCurrentCompanyProfile")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompany()
        {
            string reg = User.Identity.GetUserId();
            Company company = await db.Companies.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
            if (company == null)
            {
                return NotFound();
            }

            CompanyProfileDTO profile = new CompanyProfileDTO()
            {
                company_id = company.Id,
                company_category = company.category,
                name = company.companyName,
                email_address = company.email,
                profile_pic = company.profilePicture,
                wall_pic = company.wallpaper,
                facebook = company.fb_url,
                fax_num = company.fax,
                linkdn = company.lkdn_url,
                physical_address = company.address,
                google_plus = company.ggle_plus,
                statusMessage = company.statusMessage,
                telephone = company.tel,
                twitter = company.twt_url,
                web_url = company.website
            };

            return Ok(profile);
        }

        // tested
        /// <summary>
        /// Retrieves company profile given the ID of the company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(CompanyProfileDTO))]
        [Route("GetCompanyProfileByID")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompanyByID(int id)
        {
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            CompanyProfileDTO profile = new CompanyProfileDTO()
            {
                company_id = company.Id,
                company_category = company.category,
                name = company.companyName,
                email_address = company.email,
                profile_pic = company.profilePicture,
                wall_pic = company.wallpaper,
                facebook = company.fb_url,
                fax_num = company.fax,
                linkdn = company.lkdn_url,
                physical_address = company.address,
                google_plus = company.ggle_plus,
                statusMessage = company.statusMessage,
                telephone = company.tel,
                twitter = company.twt_url,
                web_url = company.website
            };
            return Ok(profile);
        }


        // tested
        // PUT: api/Companies/5
        /// <summary>
        /// Edit current company logged in profile
        /// </summary>
        /// <param name="company_edit"></param>
        /// <returns>returns 200 on success</returns>
        [ResponseType(typeof(void))]
        [Route("EditCompany")]
        [HttpPut]
        public async Task<IHttpActionResult> PutCompany( CompanyEditDTO company_edit)
        {
            string reg = User.Identity.GetUserId();
            Company owner = await db.Companies.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

            if (owner == null)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            int owner_id = owner.Id;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company company = new Company()
            {
                companyName = company_edit.name,
                category = company_edit.company_category,
                wallpaper = company_edit.wall_pic,
                profilePicture =company_edit.profile_pic,
                website = company_edit.web_url,
                address = company_edit.physical_address,
                fax = company_edit.fax_num,
                fb_url = company_edit.facebook,
                tel = company_edit.telephone,
                twt_url = company_edit.twitter,
                ggle_plus = company_edit.google_plus,
                statusMessage = company_edit.statusMessage,
                Id = owner_id,
                lkdn_url = company_edit.linkdn
            };

            foreach (PropertyInfo propertyInfo in owner.GetType().GetProperties())
            {
                if (propertyInfo.GetValue(company, null) == null)
                    propertyInfo.SetValue(company, propertyInfo.GetValue(owner, null), null);
            }

            try
            {
                db.Entry(owner).CurrentValues.SetValues(company);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(owner_id))
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

        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.Id == id) > 0;
        }
    }
}