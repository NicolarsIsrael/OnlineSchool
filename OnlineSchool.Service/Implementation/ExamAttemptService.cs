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
    public class ExamAttemptService : IExamAttemptService
    {
        private readonly UnitOfWork _uow;
        public ExamAttemptService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }

        public ExamAttempt CheckIfStudentAttemptAlreadyExists(int examId, int studentId)
        {
            return _uow.ExamAttemptRepo.FindInclude(ex => ex.ExamId == examId && ex.StudentId == studentId).LastOrDefault();
        }

        public async Task<ExamAttempt> CreateExamAttempt(ExamAttempt examAttempt)
        {
            _uow.ExamAttemptRepo.Add(examAttempt);
            await _uow.Save();
            return examAttempt;
        }

        public ExamAttempt Get(int id)
        {
            return _uow.ExamAttemptRepo.GetInclude(id);
        }

        public async Task Update(ExamAttempt examAttempt)
        {
            _uow.ExamAttemptRepo.Update(examAttempt);
            await _uow.Save();
        }

        public IEnumerable<ExamAttempt> GetAllExamAttempts(int examId)
        {
            return _uow.ExamAttemptRepo.GetAllInclude().Where(e => e.ExamId == examId);
        }
    }
}
