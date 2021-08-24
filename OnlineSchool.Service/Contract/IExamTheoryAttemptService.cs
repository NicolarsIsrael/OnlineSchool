using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface IExamTheoryAttemptService
    {
        ExamTheoryAttempt Get(int id);
        IEnumerable<ExamTheoryAttempt> GetAll();
        Task Add(ExamTheoryAttempt examTheoryAttempt);
        Task Updte(ExamTheoryAttempt examTheoryAttempt);
    }
}
