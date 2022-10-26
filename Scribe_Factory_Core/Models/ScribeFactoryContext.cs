using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Models
{
    public class ScribeFactoryContext : DbContext
    {
        public ScribeFactoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicatioUser> ApplicatioUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectFiles> ProjectFiles { get; set; }
        public DbSet<Subcribers> Subcribers { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Languages> Languages{ get; set; }
    }
}
