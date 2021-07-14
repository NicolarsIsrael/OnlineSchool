using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Service.Implementation
{
    public class ExamAttemptService : IExamAttemptService
    {
        private readonly UnitOfWork _uow;
        public ExamAttemptService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
    }
}
