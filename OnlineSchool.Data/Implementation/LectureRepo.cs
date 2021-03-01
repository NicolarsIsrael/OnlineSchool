using Microsoft.EntityFrameworkCore;
using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Data.Implementation
{
    public class LectureRepo : CoreRepo<Lecture>, ILectureRepo
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Lecture> dbSet;
        public LectureRepo(ApplicationDbContext ctx):base(ctx)
        {
            context = ctx;
            dbSet = context.Set<Lecture>();
        }
    }
}
