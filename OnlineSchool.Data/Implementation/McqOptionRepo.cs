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
    public class McqOptionRepo: CoreRepo<McqOption>, IMcqOptionRepo
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<McqOption> _dbSet;
        public McqOptionRepo(ApplicationDbContext ctx):base(ctx)
        {
            dbContext = ctx;
            _dbSet = dbContext.Set<McqOption>();
        }

        public McqOption GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Question)
                .FirstOrDefault();
        }

        public IEnumerable<McqOption> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Question);
        }
        public IEnumerable<McqOption> FindInclude(Expression<Func<McqOption, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(m => m.Question);
        }
    }
}
