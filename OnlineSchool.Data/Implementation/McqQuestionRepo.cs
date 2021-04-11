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
    public class McqQuestionRepo : CoreRepo<McqQuestion>, IMcqQuestionRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<McqQuestion> _dbSet;
        public McqQuestionRepo(ApplicationDbContext ctx):base(ctx)
        {
            _dbContext = ctx;
            _dbSet = _dbContext.Set<McqQuestion>();
        }

        public McqQuestion GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Options)
                .Include(c=>c.Exam)
                .FirstOrDefault();
        }

        public IEnumerable<McqQuestion> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Options)
               .Include(c=>c.Exam);
        }
        public IEnumerable<McqQuestion> FindInclude(Expression<Func<McqQuestion, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(m => m.Options)
                   .Include(m=>m.Exam);
        }
    }
}
