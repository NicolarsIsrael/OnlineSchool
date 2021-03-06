using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public Course GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c=>c.Tutor)
                .FirstOrDefault();
        }

        public IEnumerable<Course> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Tutor);
        }
        public IEnumerable<Course> FindInclude(Expression<Func<Course, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(m => m.Tutor);
        }
    }
}
