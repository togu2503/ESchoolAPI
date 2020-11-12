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
        public DbSet<Item> Items { get; set; }
        public ApplicationDBContext() : base()
        {
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : base(options)
        { }
    }
}