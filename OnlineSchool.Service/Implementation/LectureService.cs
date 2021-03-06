using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Implementation
{
    public class LectureService : ILectureService
    {
        private readonly UnitOfWork _uow;
        public LectureService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public int GetCount()
        {
            return _uow.LectureRepo.Count();
        }

        public Lecture Get(int id, bool allowNull=false)
        {
            var lecture = _uow.LectureRepo.Get(id);
            if (!allowNull && lecture == null)
                throw new Exception();
            return lecture;
        }

        public IEnumerable<Lecture> GetAll()
        {
            return _uow.LectureRepo.GetAll();
        }

        public IEnumerable<Lecture> GetAllForCourse(int courseId)
        {
            return _uow.LectureRepo.Find(l => l.CourseId == courseId);
        }

        public async Task Add(Lecture lecture)
        {
            if (!ValidateLectureDetails(lecture))
                throw new Exception();

            _uow.LectureRepo.Add(lecture);
            await _uow.Save();
        }

        public async Task Update(Lecture lecture)
        {
            if (!ValidateLectureDetails(lecture))
                throw new Exception();

            _uow.LectureRepo.Update(lecture);
            await _uow.Save();
        }

        private bool ValidateLectureDetails(Lecture lecture)
        {
            if (lecture == null)
                return false;
            if (lecture.Course == null)
                return false;
            if (string.IsNullOrEmpty(lecture.FilePath) || string.IsNullOrWhiteSpace(lecture.FilePath))
                return false;
            if (string.IsNullOrEmpty(lecture.Title) || string.IsNullOrWhiteSpace(lecture.Title))
                return false;
            return true;
        }
    }
}
