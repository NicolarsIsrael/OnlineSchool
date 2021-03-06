using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Tutor Get(int id, bool allowNull = false)
        {
            var tutor = _uow.TutorRepo.Get(id);
            if (!allowNull && tutor == null)
                throw new Exception();
            return tutor;
        }

        public Tutor GetByUserId(string userId, bool allowNull = false)
        {
            var tutor = _uow.TutorRepo.FindInclude(t=>t.UserId==userId).FirstOrDefault();
            if (!allowNull && tutor == null)
                throw new Exception();
            return tutor;
        }

        public IEnumerable<Tutor> GetAll()
        {
            return _uow.TutorRepo.GetAll();
        }

        public async Task Add(Tutor tutor)
        {
            if (!ValidateTutorDetails(tutor))
                throw new Exception();

            _uow.TutorRepo.Add(tutor);
            await _uow.Save();
        }

        public async Task Update(Tutor tutor)
        {
            if (!ValidateTutorDetails(tutor))
                throw new Exception();

            _uow.TutorRepo.Update(tutor);
            await _uow.Save();
        }

        private bool ValidateTutorDetails(Tutor tutor)
        {
            if (tutor == null)
                return false;
            if (string.IsNullOrEmpty(tutor.Email) || string.IsNullOrWhiteSpace(tutor.Email))
                return false;
            if (string.IsNullOrEmpty(tutor.UserId) || string.IsNullOrWhiteSpace(tutor.UserId))
                return false;
            if (string.IsNullOrEmpty(tutor.Fullname) || string.IsNullOrWhiteSpace(tutor.Fullname))
                return false;
            return true;
        }
    }
}
