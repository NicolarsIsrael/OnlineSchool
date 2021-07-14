using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Data.Implementation
{
    public class ExamMcqAttemptRepo : CoreRepo<ExamAttempt>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ExamAttempt> _dbSet;
        public ExamMcqAttemptRepo(ApplicationDbContext ctx):base(ctx)
        {
            _context = ctx;
            _dbSet = _context.Set<ExamAttempt>();
        }
    }
}
