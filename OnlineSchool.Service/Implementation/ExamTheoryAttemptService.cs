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
    public class ExamTheoryAttemptService : IExamTheoryAttemptService
    {
        private readonly UnitOfWork _uow;
        public ExamTheoryAttemptService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public async Task Add(ExamTheoryAttempt examTheoryAttempt)
        {
            _uow.ExamTheoryAttemptRepo.Add(examTheoryAttempt);
            await _uow.Save();
        }

        public ExamTheoryAttempt Get(int id)
        {
            return _uow.ExamTheoryAttemptRepo.Get(id);
        }

        public IEnumerable<ExamTheoryAttempt> GetAll()
        {
            return _uow.ExamTheoryAttemptRepo.GetAll();
        }

        public async Task Updte(ExamTheoryAttempt examTheoryAttempt)
        {
            _uow.ExamTheoryAttemptRepo.Update(examTheoryAttempt);
            await _uow.Save();
        }
    }
}
