using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface IExamMcqAttemptService
    {
        ExamMcqAttempt Get(int id);
        Task Update(ExamMcqAttempt examMcqAttempt);
    }
}
