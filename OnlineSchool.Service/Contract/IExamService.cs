using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface IExamService
    {
        Task Add(Exam exam);
        IEnumerable<Exam> GetAll();
        IEnumerable<Exam> GetForCourse(int courseId);
    }
}
