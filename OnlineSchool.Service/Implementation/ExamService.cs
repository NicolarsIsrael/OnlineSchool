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
    public class ExamService : IExamService
    {
        private readonly UnitOfWork _uow;
        public ExamService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }

        public async Task Add(Exam exam)
        {
            _uow.ExamRepo.Add(exam);
            await _uow.Save();
        }

        public async Task Update(Exam exam)
        {
            _uow.ExamRepo.Update(exam);
            await _uow.Save();
        }

        public Exam Get(int id, bool allowNull = false)
        {
            var exam = _uow.ExamRepo.GetInclude(id);
            if (exam == null && !allowNull)
                throw new Exception();
            return exam;
        }

        public IEnumerable<Exam> GetAll()
        {
            return _uow.ExamRepo.GetAllInclude();
        }

        public IEnumerable<Exam> GetForCourse(int courseId)
        {
            return _uow.ExamRepo.FindInclude(e => e.Course.Id == courseId);
        }
    }
}
