using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using restApi.Models;
namespace restApi.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base()
        {
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : base(options)
        { }
        public DbSet<restApi.Models.Pupil> Pupil { get; set; }
        public DbSet<restApi.Models.User> User { get; set; }
        public DbSet<restApi.Models.ActiveUser> ActiveUser { get; set; }
    }
}