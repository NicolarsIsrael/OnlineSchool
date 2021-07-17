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
    public class ExamMcqAttemptService : IExamMcqAttemptService
    {
        private readonly UnitOfWork _uow;
        public ExamMcqAttemptService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }

        public ExamMcqAttempt Get(int id)
        {
            return _uow.ExamMcqAttemptRepo.Get(id);
        }

        public async Task Update(ExamMcqAttempt examMcqAttempt)
        {
            _uow.ExamMcqAttemptRepo.Update(examMcqAttempt);
            await _uow.Save();
        }
    }
}
