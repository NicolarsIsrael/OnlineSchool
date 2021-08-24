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
            try
            {
                var exam = _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                       .Include(c => c.Course)
                       .Include(c => c.MultiChoiceQuestions)
                           .ThenInclude(c => c.Options)
                        .Include(c=>c.TheoryQuestions)
                       .FirstOrDefault();
                //exam.MultiChoiceQuestions = exam.MultiChoiceQuestions.Where(mcq => !mcq.IsDeleted).ToList();
                return exam;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Exam> GetAllInclude()
        {
            var exams = _dbSet.Where(c => !c.IsDeleted)
               .Include(c => c.Course)
                .Include(c => c.MultiChoiceQuestions)
                    .ThenInclude(c => c.Options)
                .Include(c => c.TheoryQuestions);

            foreach(var exam in exams)
                exam.MultiChoiceQuestions = exam.MultiChoiceQuestions.Where(mcq => !mcq.IsDeleted).ToList();
            return exams;
        }
        public IEnumerable<Exam> FindInclude(Expression<Func<Exam, bool>> predicate)
        {
            var exams = _dbSet.Where(predicate).Where(t => t.IsDeleted == false)
                   .Include(m => m.Course)
                  .Include(c => c.TheoryQuestions)
                .Include(c => c.MultiChoiceQuestions)
                    .ThenInclude(c => c.Options);

            foreach (var exam in exams)
                exam.MultiChoiceQuestions = exam.MultiChoiceQuestions.Where(mcq => !mcq.IsDeleted).ToList();
            return exams;
        }
    }
}
