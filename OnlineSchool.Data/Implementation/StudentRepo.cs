using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineSchool.Data.Implementation
{
    public class StudentRepo : CoreRepo<Student>, IStudentRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Student> _dbSet;
        public StudentRepo(ApplicationDbContext ctx): base(ctx)
        {
            _context = ctx;
            _dbSet = _context.Set<Student>();
        }
        public Student GetInclude(int id)
        {
            return _dbSet.Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Courses)
                        .ThenInclude(s=>s.Tutor)
                .FirstOrDefault();
        }

        public IEnumerable<Student> GetAllInclude()
        {
            return _dbSet.Where(c => !c.IsDeleted)
                .Include(c => c.Courses)
                        .ThenInclude(s => s.Tutor);
        }
    }
}
