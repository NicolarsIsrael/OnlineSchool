using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Data.Implementation
{
    public class TutorRepo : CoreRepo<Tutor>, ITutorRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Tutor> _dbSet;
        public TutorRepo(ApplicationDbContext ctx):base(ctx)
        {
            _context = ctx;
            _dbSet = _context.Set<Tutor>();
        }
    }
}
