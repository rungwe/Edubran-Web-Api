using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Web.Http.Description;
using System.Threading.Tasks;
using EdubranApi.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace EdubranApi.Controllers
{
    /// <summary>
    /// testing file uploads
    /// </summary>
    [RoutePrefix("api/upload")]
    public class UploadsController : ApiController
    {
        private EdubranApiContext db = new EdubranApiContext();

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("uploadImage")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile =  HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = HttpContext.Current.Request.Files["UploadedImage"].InputStream;
                
                if (httpPostedFile != null)
                {
                    bool status = UploadFileToS3("testing2.png", img, "edu-media");
                    if (status)
                    {
                        return StatusCode(HttpStatusCode.OK);
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.NotAcceptable);
                    }
                    
                    
                }
                else
                {
                    return StatusCode(HttpStatusCode.ExpectationFailed);
                }
            }
            else
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }
        }
        /// <summary>
        /// Upload the profile picture of the current user, tested
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("profilePic")]
        [ResponseType(typeof(FileDTO))]
        public async Task<IHttpActionResult> profilePic()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string reg = User.Identity.GetUserId();
                Company company = await db.Companies.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
                Student student = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = httpPostedFile.InputStream;
                if (student != null)
                {


                    if (httpPostedFile != null)
                    {
                        string oldname = student.profilePic;
                        string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ","-");

                        bool status = UploadFileToS3(filename, img, "edu-media");
                        if (status)
                        {
                            student.profilePic = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                            db.Entry(student).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            if (oldname != null)
                            {
                                deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                            }
                            return Ok(new FileDTO() { filename = student.profilePic });
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.NotAcceptable);
                        }
                    }
                }

                else if (company != null)
                {

                    if (httpPostedFile != null)
                    {
                        string oldname = company.profilePicture;
                        string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName;
                        bool status = UploadFileToS3(filename, img, "edu-media");
                        if (status)
                        {
                            company.profilePicture = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                            db.Entry(company).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            if (oldname != null)
                            {
                                deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                            }
                            
                            return Ok(new FileDTO() { filename = company.profilePicture });
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.NotAcceptable);
                        }
                    }
                }

                    
                
            }

           return StatusCode(HttpStatusCode.ExpectationFailed);
            
        }


        /// <summary>
        /// Upload the wallpaper of the current user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("wallPaper")]
        [ResponseType(typeof(FileDTO))]
        public async Task<IHttpActionResult> wallpaper()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string reg = User.Identity.GetUserId();
                Company company = await db.Companies.Where(d => d.registrationId == reg).SingleOrDefaultAsync();
                Student student = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = httpPostedFile.InputStream;
                
                if (student != null)
                {


                    if (httpPostedFile != null)
                    {
                        string oldname = student.wallpaper;
                        string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ", "-");

                        bool status = UploadFileToS3(filename, img, "edu-media");
                        if (status)
                        {
                            student.wallpaper = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                            db.Entry(student).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            if (oldname != null)
                            {
                                deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                            }
                            return Ok(new FileDTO() { filename = student.wallpaper });
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.NotAcceptable);
                        }
                    }
                }

                else if (company != null)
                {

                    if (httpPostedFile != null)
                    {
                        string oldname = company.wallpaper;
                        string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ", "-");
                        bool status = UploadFileToS3(filename, img, "edu-media");
                        if (status)
                        {
                            company.wallpaper = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                            db.Entry(company).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            if (oldname != null)
                            {
                                deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                            }

                            return Ok(new FileDTO() { filename = company.wallpaper });
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.NotAcceptable);
                        }
                    }
                }



            }

            return StatusCode(HttpStatusCode.ExpectationFailed);

        }

        /// <summary>
        /// Upload the project image
        /// </summary>
        /// <param name="project_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("projectPic")]
        [ResponseType(typeof(FileDTO))]
        public async Task<IHttpActionResult> project_picture(int project_id)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string reg = User.Identity.GetUserId();
                Company owner = await db.Companies.FirstAsync(b => b.registrationId == reg);
                int owner_id = owner.Id;

                Project project = await db.Projects.FindAsync(project_id);
                if (project == null)
                {
                    return NotFound();
                }
                if (project.companyId != owner_id)
                {
                    return StatusCode(HttpStatusCode.NotAcceptable);
                }

                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = httpPostedFile.InputStream;

                if (httpPostedFile != null)
                {
                    string oldname = project.post_pic;
                    string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ", "-");

                    bool status = UploadFileToS3(filename, img, "edu-media");
                    if (status)
                    {
                        project.post_pic = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                        db.Entry(project).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        if (oldname != null)
                        {
                            deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                        }
                        return Ok(new FileDTO() { filename = project.post_pic });
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.NotAcceptable);
                    }
                }

            }

            return StatusCode(HttpStatusCode.ExpectationFailed);

        }

        /// <summary>
        /// Upload Project additional attachment, eg a pdf to further describe the project, restricted only to the owner of the project
        /// </summary>
        /// <param name="project_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("projectAttachment")]
        [ResponseType(typeof(FileDTO))]
        public async Task<IHttpActionResult> project_attachment(int project_id)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string reg = User.Identity.GetUserId();
                Company owner = await db.Companies.FirstAsync(b => b.registrationId == reg);
                int owner_id = owner.Id;

                Project project = await db.Projects.FindAsync(project_id);
                if (project == null)
                {
                    return NotFound();
                }
                if (project.companyId != owner_id)
                {
                    return StatusCode(HttpStatusCode.NotAcceptable);
                }

                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = httpPostedFile.InputStream;

                if (httpPostedFile != null)
                {
                    string oldname = project.detailsResourceUrl;
                    string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ", "-");

                    bool status = UploadFileToS3(filename, img, "edu-media");
                    if (status)
                    {
                        project.detailsResourceUrl = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                        db.Entry(project).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        if (oldname != null)
                        {
                            deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                        }
                        return Ok(new FileDTO() { filename = project.detailsResourceUrl });
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.NotAcceptable);
                    }
                }

            }

            return StatusCode(HttpStatusCode.ExpectationFailed);

        }


        /// <summary>
        /// Upload the transcripts, only for students
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("transcripts")]
        [ResponseType(typeof(FileDTO))]
        public async Task<IHttpActionResult> transcripts()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string reg = User.Identity.GetUserId();
                
                Student student = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = httpPostedFile.InputStream;
                if (student != null)
                {


                    if (httpPostedFile != null)
                    {
                        string oldname = student.transcripts;
                        string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ", "-");

                        bool status = UploadFileToS3(filename, img, "edu-media");
                        if (status)
                        {
                            student.transcripts = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                            db.Entry(student).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            if (oldname != null)
                            {
                                deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                            }
                            return Ok(new FileDTO() { filename = student.transcripts });
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.NotAcceptable);
                        }
                    }
                }

                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);

                }



            }

            return StatusCode(HttpStatusCode.ExpectationFailed);

        }




        /// <summary>
        /// Upload the transcripts, only for students
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("curriculumVitae")]
        [ResponseType(typeof(FileDTO))]
        public async Task<IHttpActionResult> curriculumVitae()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string reg = User.Identity.GetUserId();

                Student student = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = httpPostedFile.InputStream;
                if (student != null)
                {


                    if (httpPostedFile != null)
                    {
                        string oldname = student.cv;
                        string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ", "-");

                        bool status = UploadFileToS3(filename, img, "edu-media");
                        if (status)
                        {
                            student.cv = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                            db.Entry(student).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            if (oldname != null)
                            {
                                deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                            }
                            return Ok(new FileDTO() { filename = student.cv });
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.NotAcceptable);
                        }
                    }
                }

                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);

                }



            }

            return StatusCode(HttpStatusCode.ExpectationFailed);

        }




        /// <summary>
        /// Upload the transcripts, only for students
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("identityDoc")]
        [ResponseType(typeof(FileDTO))]
        public async Task<IHttpActionResult> identity_document()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string reg = User.Identity.GetUserId();

                Student student = await db.Students.Where(d => d.registrationId == reg).SingleOrDefaultAsync();

                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                Stream img = httpPostedFile.InputStream;
                if (student != null)
                {


                    if (httpPostedFile != null)
                    {
                        string oldname = student.national_id;
                        string filename = Guid.NewGuid().ToString() + httpPostedFile.FileName.Replace(" ", "-");

                        bool status = UploadFileToS3(filename, img, "edu-media");
                        if (status)
                        {
                            student.national_id = "https://s3-us-west-2.amazonaws.com/edu-media/" + filename;
                            db.Entry(student).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            if (oldname != null)
                            {
                                deleteFile(oldname.Replace("https://s3-us-west-2.amazonaws.com/edu-media/", ""), "edu-media");
                            }
                            return Ok(new FileDTO() { filename = student.national_id });
                        }
                        else
                        {
                            return StatusCode(HttpStatusCode.NotAcceptable);
                        }
                    }
                }

                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);

                }



            }

            return StatusCode(HttpStatusCode.ExpectationFailed);

        }



        [ApiExplorerSettings(IgnoreApi = true)]
        public bool UploadFileToS3(string uploadAsFileName, Stream ImageStream, string toWhichBucketName)
        {

            try
            {
                var client = Amazon.AWSClientFactory.CreateAmazonS3Client("AKIAIM2K2FSR2DC6KMSA", "yZ2+Mzs8w1iYGPplXNkLfWtOqif9Rad5wjGu3/zn", Amazon.RegionEndpoint.USWest2);
                PutObjectRequest request = new PutObjectRequest();
                request.Key=uploadAsFileName;
                request.InputStream=ImageStream;
                request.BucketName= toWhichBucketName;
                request.CannedACL = S3CannedACL.PublicRead;
                request.StorageClass = S3StorageClass.Standard;
                client.PutObject(request);
                client.Dispose();
            }
            catch (Exception e)
            {

                return false;
            }
            return true;

        }


        public bool deleteFile(string keyName, string bucketName)
        {

            try
            {
                IAmazonS3 client;
                client = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);

                DeleteObjectRequest deleteObjectRequest =
                    new DeleteObjectRequest
                    {
                        BucketName = bucketName,
                        Key = keyName
                    };

                using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(
                     "AKIAIM2K2FSR2DC6KMSA", "yZ2+Mzs8w1iYGPplXNkLfWtOqif9Rad5wjGu3/zn", Amazon.RegionEndpoint.USWest2))
                {
                    var response = client.DeleteObject(deleteObjectRequest);

                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
    }

    

}
