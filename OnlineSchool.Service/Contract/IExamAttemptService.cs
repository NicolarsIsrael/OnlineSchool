using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface IExamAttemptService
    {
        ExamAttempt CheckIfStudentAttemptAlreadyExists(int examId, int studentId);
        Task<ExamAttempt> CreateExamAttempt(ExamAttempt examAttempt);
        Task Update(ExamAttempt examAttempt);
        ExamAttempt Get(int id);
    }
}
