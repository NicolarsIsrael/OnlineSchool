using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface IMcqQuestionService
    {
        Task Add(McqQuestion mcq);
        IEnumerable<McqQuestion> GetAllForExam(int examId);
        McqQuestion GetById(int id);
        Task Update(McqQuestion mcq);
        Task Delete(McqQuestion mcq);
    }
}
