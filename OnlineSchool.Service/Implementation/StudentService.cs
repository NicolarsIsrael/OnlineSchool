using OnlineSchool.Core;
using OnlineSchool.Data.Contract;
using OnlineSchool.Data.Implementation;
using OnlineSchool.Service.Contract;
using OnlineSchool.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly UnitOfWork _uow;
        public StudentService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }
        public int GetCount()
        {
            return _uow.StudentRepo.Count();
        }

        public Student Get(int id, bool allowNull = false)
        {
            var student = _uow.StudentRepo.Get(id);
            if (!allowNull && student == null)
                throw new Exception();
            return student;
        }

        public Student GetByUserId(string userId, bool allowNull = false)
        {
            var student = _uow.StudentRepo.GetAllInclude().Where(s => s.UserId == userId).FirstOrDefault();
            if (!allowNull && student == null)
                throw new Exception();
            return student;
        }

        public IEnumerable<Student> GetAll()
        {
            return _uow.StudentRepo.GetAll();
        }

        public async Task Add(Student student)
        {
            if (!ValidateStudentDetails(student))
                throw new Exception();

            _uow.StudentRepo.Add(student);
            await _uow.Save();
            student.MatricNumber = GenerateMatricNumber(student.Id);
            await Update(student);
        }

        public async Task Update(Student student)
        {
            if (!ValidateStudentDetails(student))
                throw new Exception();

            _uow.StudentRepo.Update(student);
            await _uow.Save();
        }

        private bool ValidateStudentDetails(Student student)
        {
            if (student == null)
                return false;
            if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrWhiteSpace(student.FirstName))
                return false;
            if (string.IsNullOrEmpty(student.LastName) || string.IsNullOrWhiteSpace(student.LastName))
                return false;
            if (string.IsNullOrEmpty(student.Email) || string.IsNullOrWhiteSpace(student.Email))
                return false;
            if (string.IsNullOrEmpty(student.ProfilePicturePath) || string.IsNullOrWhiteSpace(student.ProfilePicturePath))
                return false;
            if (string.IsNullOrEmpty(student.UserId) || string.IsNullOrWhiteSpace(student.UserId))
                return false;
            return true;
        }

        private string GenerateMatricNumber(int studentId)
        {
            string studentNumber = $"{studentId}";
            if (studentNumber.Length == 1)
                studentNumber = "00" + studentNumber;
            else if (studentNumber.Length == 2)
                studentNumber = "0" + studentNumber;

            return AppConstant.MatricNumberStarter + studentNumber;
        }
    }
}
