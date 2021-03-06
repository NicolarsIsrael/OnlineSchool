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
    public class TutorRepo : CoreRepo<Tutor>, ITutorRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Tutor> _dbSet;
        public TutorRepo(ApplicationDbContext ctx):base(ctx)
        {
            _context = ctx;
            _dbSet = _context.Set<Tutor>();
        }

        public Tutor GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Courses)
                .FirstOrDefault();
        }

        public IEnumerable<Tutor> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Courses);
        }
        public IEnumerable<Tutor> FindInclude(Expression<Func<Tutor, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(m => m.Courses);
        }
    }
}
