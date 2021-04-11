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
    public class ExamRepo : CoreRepo<Exam>, IExamRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Exam> _dbSet;
        public ExamRepo(ApplicationDbContext ctx):base(ctx)
        {
            _dbContext = ctx;
            _dbSet = _dbContext.Set<Exam>();
        }

        public Exam GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Course)
                .FirstOrDefault();
        }

        public IEnumerable<Exam> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Course);
        }
        public IEnumerable<Exam> FindInclude(Expression<Func<Exam, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(m => m.Course);
        }
    }
}
