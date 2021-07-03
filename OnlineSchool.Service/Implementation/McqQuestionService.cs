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
    public class McqQuestionService : IMcqQuestionService
    {
        private readonly UnitOfWork _uow;
        public McqQuestionService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }

        public async Task Add(McqQuestion mcq)
        {
            _uow.McqQuestionRepo.Add(mcq);
            mcq.Exam.TotalScore += mcq.Score;
            _uow.ExamRepo.Update(mcq.Exam);
            await _uow.Save();
        }

        public IEnumerable<McqQuestion> GetAllForExam(int examId)
        {
            return _uow.McqQuestionRepo.FindInclude(m => m.ExamId == examId);
        }

        public McqQuestion GetById(int id)
        {
            return _uow.McqQuestionRepo.GetInclude(id);
        }

        public async Task Update(McqQuestion mcq)
        {
             _uow.McqQuestionRepo.Update(mcq);
            await _uow.Save();
        }
    }
}
