using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Data.Contract
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
