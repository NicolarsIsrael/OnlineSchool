using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface ILectureService
    {
        int GetCount();
        IEnumerable<Lecture> GetAll();
        IEnumerable<Lecture> GetAllForCourse(int courseId);
        Lecture Get(int id, bool allowNull = false);
        Task Add(Lecture lecture);
        Task Update(Lecture lecture);
    }
}
