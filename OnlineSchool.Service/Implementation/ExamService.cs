using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Service.Implementation
{
    public class ExamService : IExamService
    {
        private readonly UnitOfWork _uow;
        public ExamService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
    }
}
