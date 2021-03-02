using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface ICourseService
    {
        int GetCount();
        Course Get(int id, bool allowNull = false);
        IEnumerable<Course> GetAll();
        Course GetByCourseCode(string courseCode);
        Task Add(Course course);
        Task Update(Course course);
    }
}
