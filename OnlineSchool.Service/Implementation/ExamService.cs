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
