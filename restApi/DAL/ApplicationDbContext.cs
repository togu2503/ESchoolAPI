using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
        public DbSet<Pupil> Pupil { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ActiveUser> ActiveUser { get; set; }
        public DbSet<Curiculum> Curriculum { get; set; }
        public DbSet<Homework> Homework { get; set; }
        public DbSet<Lesson> Lesson{ get; set; }
        public DbSet<PupilFormJoint> PupilFormJoints { get; set; }
        public DbSet<Form> Form { get; set; }
    }
}