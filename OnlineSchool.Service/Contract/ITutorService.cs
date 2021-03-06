using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface ITutorService
    {
        int GetCount();
        Tutor GetByUserId(string userId, bool allowNull = false);
        IEnumerable<Tutor> GetAll();
        Tutor Get(int id, bool allowNull = false);
        Task Add(Tutor tutor);
        Task Update(Tutor tutor);
    }
}