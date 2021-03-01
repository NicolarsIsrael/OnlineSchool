using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly UnitOfWork _uow;
        public StudentService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public int GetCount()
        {
            return _uow.StudentRepo.Count();
        }
    }
}
