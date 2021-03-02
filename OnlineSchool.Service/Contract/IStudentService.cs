using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        int GetCount();
        Student Get(int id, bool allowNull = false);
        Task Add(Student student);
        Task Update(Student student);
    }
}
