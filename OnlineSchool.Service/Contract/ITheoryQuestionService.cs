using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface ITheoryQuestionService
    {
        TheoryQuestion Get(int id);
        IEnumerable<TheoryQuestion> Get();
        Task Add(TheoryQuestion theoryQuestion);
        Task Update(TheoryQuestion theoryQuestion, Exam exam);
        Task Delete(TheoryQuestion theoryQuestion, Exam exam);

    }
}
