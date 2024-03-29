﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Tutor> Tutor { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Lecture> Lecture { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<McqOption> McqOption { get; set; }
        public DbSet<McqQuestion> McqQuestion { get; set; }
        public DbSet<ExamAttempt> ExamAttempt { get; set; }
        public DbSet<ExamMcqAttempt> ExamMcqAttempt { get; set; }
    }
}
