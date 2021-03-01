using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Service.Implementation
{
    public class TutorService : ITutorService
    {
        private readonly UnitOfWork _uow;
        public TutorService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public int GetCount()
        {
            return _uow.TutorRepo.Count();
        }
    }
}
