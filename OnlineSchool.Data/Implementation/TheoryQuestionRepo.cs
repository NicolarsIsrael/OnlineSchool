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
    public class TheoryQuestionRepo : CoreRepo<TheoryQuestion>, ITheoryQuestionRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TheoryQuestion> _dbSet;
        public TheoryQuestionRepo(ApplicationDbContext ctx):base(ctx)
        {
            _dbContext = ctx;
            _dbSet = _dbContext.Set<TheoryQuestion>();
        }

        public TheoryQuestion GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Exam)
                    .ThenInclude(e => e.Course)
                .FirstOrDefault();
        }

        public IEnumerable<TheoryQuestion> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Exam)
                    .ThenInclude(e => e.Course);
        }
        public IEnumerable<TheoryQuestion> FindInclude(Expression<Func<TheoryQuestion, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(m => m.Exam)
                    .ThenInclude(e => e.Course);
        }
    }
}
