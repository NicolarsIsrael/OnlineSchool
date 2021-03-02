using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Core;
using OnlineSchool.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class ViewStudentsModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        [Display(Name ="First name")]
        public string Firstname { get; set; }
        [Display(Name ="Last name")]
        public string Lastname { get; set; }
        [Display(Name ="Matric number")]
        public string MatricNumber { get; set; }
        [Display(Name ="Profile picture")]
        public string ProfilePicture { get; set; }
        public ViewStudentsModel(Student student)
        {
            Id = student.Id;
            UserId = student.UserId;
            Email = student.Email;
            Firstname = student.FirstName;
            Lastname = student.LastName;
            MatricNumber = student.MatricNumber;
            ProfilePicture =GeneralFunction.GetUrlPath(student.ProfilePicturePath);
        }
    }

    public class AddStudentModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("CheckIfEmailExists", "Admin", ErrorMessage = "Email already exists")]
        public string Email { get; set; }

        [Display(Name ="First name")]
        [Required(ErrorMessage ="First name is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "First name should begin with letter or number")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Last name should begin with letter or number")]
        public string LastName { get; set; }
        public IFormFile ProfileImage { get; set; }
        public Student Add(string userId, string url)
        {
            return new Student
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                ProfilePicturePath = url,
                UserId = userId,
            };
        }
    }
}
