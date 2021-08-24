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
            mcq.Exam.TotalScore = mcq.Exam.TotalScore + mcq.Score;
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

        public async Task Update(McqQuestion mcq, Exam exam)
        {
             _uow.McqQuestionRepo.Update(mcq);
            exam.TotalScore = exam.MultiChoiceQuestions.Where(e => !e.IsDeleted).Sum(m => m.Score) 
                + exam.TheoryQuestions.Where(e => !e.IsDeleted).Sum(m => m.Score);
            _uow.ExamRepo.Update(exam);
            await _uow.Save();
        }

        public async Task Delete(McqQuestion mcq, Exam exam)
        {
            _uow.McqQuestionRepo.Remove(mcq);
            exam.TotalScore = exam.MultiChoiceQuestions.Where(e => !e.IsDeleted).Sum(m => m.Score)
                 + exam.TheoryQuestions.Where(e => !e.IsDeleted).Sum(m => m.Score);

            await _uow.Save();
        }
    }
}
