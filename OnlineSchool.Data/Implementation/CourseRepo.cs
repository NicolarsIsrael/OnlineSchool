using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Data.Implementation
{
    public class CourseRepo : CoreRepo<Course>, ICourseRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Course> _dbSet;
        public CourseRepo(ApplicationDbContext ctx):base(ctx)
        {
            _context = ctx;
            _dbSet = _context.Set<Course>();
        }
    }
}
