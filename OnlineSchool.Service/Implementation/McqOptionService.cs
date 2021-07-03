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
    public class McqOptionService : IMcqOptionService
    {
        private readonly UnitOfWork _uow;
        public McqOptionService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }

        public McqOption Get(int id)
        {
            return _uow.McqOptionRepo.Get(id);
        }

        public async Task Update(McqOption mcqOption)
        {
            _uow.McqOptionRepo.Update(mcqOption);
            await _uow.Save();
        }
    }
}
