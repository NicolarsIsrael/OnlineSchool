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
    public class TheoryQuestionService : ITheoryQuestionService
    {
        private readonly UnitOfWork _uow;
        public TheoryQuestionService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public async Task Add(TheoryQuestion theoryQuestion)
        {
            _uow.TheoryQuestionRepo.Add(theoryQuestion);
            theoryQuestion.Exam.TotalScore = theoryQuestion.Exam.TotalScore + theoryQuestion.Score;
            _uow.ExamRepo.Update(theoryQuestion.Exam);
            await _uow.Save();
        }

        public async Task Delete(TheoryQuestion theoryQuestion, Exam exam)
        {
            _uow.TheoryQuestionRepo.Remove(theoryQuestion);
            exam.TotalScore = exam.MultiChoiceQuestions.Where(e => !e.IsDeleted).Sum(m => m.Score)
                 + exam.TheoryQuestions.Where(e => !e.IsDeleted).Sum(m => m.Score);
            await _uow.Save();
        }

        public TheoryQuestion Get(int id)
        {
            return _uow.TheoryQuestionRepo.GetInclude(id);
        }

        public IEnumerable<TheoryQuestion> Get()
        {
            return _uow.TheoryQuestionRepo.GetAllInclude();
        }

        public async Task Update(TheoryQuestion theoryQuestion, Exam exam)
        {
            _uow.TheoryQuestionRepo.Update(theoryQuestion);
            exam.TotalScore = exam.MultiChoiceQuestions.Sum(m => m.Score);
            exam.TotalScore += exam.TheoryQuestions.Sum(m => m.Score);
            await _uow.Save();
        }
    }
}
