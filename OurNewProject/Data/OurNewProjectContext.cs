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
    }
}
