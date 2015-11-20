using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdubranApi.Models
{
    public class EdubranApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EdubranApiContext() : base("name=EdubranApiContext")
        {
        }

        public System.Data.Entity.DbSet<EdubranApi.Models.Application> Applications { get; set; }

        public System.Data.Entity.DbSet<EdubranApi.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<EdubranApi.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<EdubranApi.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<EdubranApi.Models.Skill> Skills { get; set; }

        public System.Data.Entity.DbSet<EdubranApi.Models.Comment> Comments { get; set; }
    }
}
