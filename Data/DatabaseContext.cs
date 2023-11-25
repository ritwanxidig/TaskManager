using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Models;
using Task = Task_Manager.Models.Task;

namespace Task_Manager.Data
{
    public class DatabaseContext : IdentityDbContext
    {
        // public DbSet<PreviousUser>? PreviousUsers { get; set; }
        public DbSet<Task>? Tasks { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Project>? Projects { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }


    }
}