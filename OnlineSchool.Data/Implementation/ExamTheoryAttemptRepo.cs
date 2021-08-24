using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Data.Implementation
{
    public class ExamTheoryAttemptRepo : CoreRepo<ExamTheoryAttempt>, IExamTheoryAttemptRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ExamTheoryAttempt> _dbSet;
        public ExamTheoryAttemptRepo(ApplicationDbContext ctx):base(ctx)
        {
            _dbContext = ctx;
            _dbSet = _dbContext.Set<ExamTheoryAttempt>();
        }


    }
}
