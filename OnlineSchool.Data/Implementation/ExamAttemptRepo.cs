using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OnlineSchool.Data.Implementation
{
    public class ExamAttemptRepo : CoreRepo<ExamAttempt>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ExamAttempt> _dbSet;
        public ExamAttemptRepo(ApplicationDbContext ctx):base(ctx)
        {
            _context = ctx;
            _dbSet = _context.Set<ExamAttempt>();
        }


        public ExamAttempt GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Mcqs)
                    .ThenInclude(mcq=>mcq.McqOptions)
                .Include(c=>c.Student)
                .FirstOrDefault();
        }

        public IEnumerable<ExamAttempt> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Mcqs)
                    .ThenInclude(mcq => mcq.McqOptions)
                .Include(c => c.Student);
        }
        public IEnumerable<ExamAttempt> FindInclude(Expression<Func<ExamAttempt, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(c => c.Mcqs)
                    .ThenInclude(mcq => mcq.McqOptions)
                .Include(c => c.Student);
        }
    }
}
