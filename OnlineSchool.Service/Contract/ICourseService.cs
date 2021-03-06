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
        Course GetForTutor(int id, int tutorId, bool allowNull = false);
        IEnumerable<Course> GetAll();
        IEnumerable<Course> GetAllForTutor(int tutorId);
        Course GetByCourseCode(string courseCode);
        Task Add(Course course);
        Task Update(Course course);
        bool CheckIfStudentOfferCourse(Student student, Course course);
    }
}
