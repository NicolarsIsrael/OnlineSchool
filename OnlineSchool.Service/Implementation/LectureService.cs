using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Service.Implementation
{
    public class LectureService : ILectureService
    {
        private readonly UnitOfWork _uow;
        public LectureService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public int GetCount()
        {
            return _uow.LectureRepo.Count();
        }
    }
}
