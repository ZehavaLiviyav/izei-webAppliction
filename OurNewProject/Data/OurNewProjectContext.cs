using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurNewProject.Models;

namespace OurNewProject.Data
{
    public class OurNewProjectContext : DbContext
    {
        public OurNewProjectContext (DbContextOptions<OurNewProjectContext> options)
            : base(options)
        {
        }
        public DbSet<OurNewProject.Models.User> User { get; set; }
        public DbSet<OurNewProject.Models.Product> Product { get; set; }
        public DbSet<OurNewProject.Models.ProductImage> ProductImage { get; set; }
        public DbSet<OurNewProject.Models.Order> Order { get; set; }
        public DbSet<OurNewProject.Models.Category> Category { get; set; }
        public DbSet<OurNewProject.Models.Branch> Branch { get; set; }

       // public DbSet<OurNewProject.Models.User> User { get; set; }
    }
}
