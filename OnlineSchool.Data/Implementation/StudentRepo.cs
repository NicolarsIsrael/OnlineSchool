using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using System;
using System.Collections.Generic;
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
    }
}
