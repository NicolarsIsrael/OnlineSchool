using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly UnitOfWork _uow;
        public CourseService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public int GetCount()
        {
            return _uow.CourseRepo.Count();
        }

        public Course Get(int id, bool allowNull = false)
        {
            var course = _uow.CourseRepo.GetInclude(id);
            if (!allowNull && course == null)
                throw new Exception();
            return course;
        }

        public IEnumerable<Course> GetAll()
        {
            return _uow.CourseRepo.GetAllInclude();
        }

        public Course GetByCourseCode(string courseCode)
        {
            return _uow.CourseRepo.GetAll().Where(c => string.Compare(c.CourseCode, courseCode, true) == 0).ToList().FirstOrDefault();
        }

        public async Task Add(Course course)
        {
            if (!ValidateCourseDetails(course))
                throw new Exception();

             _uow.CourseRepo.Add(course);
            await _uow.Save();
        }

        public async Task Update(Course course)
        {
            if (!ValidateCourseDetails(course))
                throw new Exception();

            _uow.CourseRepo.Update(course);
            await _uow.Save();
        }

        private bool ValidateCourseDetails(Course course)
        {
            if (course == null)
                return false;
            if (course.Tutor == null)
                return false;
            if (string.IsNullOrEmpty(course.CourseTitle) || string.IsNullOrWhiteSpace(course.CourseTitle))
                return false;
            if (string.IsNullOrEmpty(course.CourseCode) || string.IsNullOrWhiteSpace(course.CourseCode))
                return false;
            return true;
        }
    }
}
