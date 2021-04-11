using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class TutorCourseModel
    {
        public int NumberOfLectures { get; set; }
        public int NumberOfExams { get; set; }
        public IEnumerable<ViewLectureModel> Lectures { get; set; }
        public IEnumerable<ViewExamModel> Exams { get; set; }

        public TutorCourseModel(IEnumerable<ViewLectureModel> lectures, IEnumerable<ViewExamModel> exams)
        {
            Exams = exams;
            Lectures = lectures;
            NumberOfExams = exams.Count();
            NumberOfLectures = lectures.Count();
        }
    }

    public class ViewTutorModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        [Display(Name = "Full name")]
        public string Fullname { get; set; }
        public ViewTutorModel(Tutor tutor)
        {
            Id = tutor.Id;
            UserId = tutor.UserId;
            Email = tutor.Email;
            Fullname = tutor.Fullname;
        }
    }

    public class AddTutorModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("CheckIfEmailExists", "Admin", ErrorMessage = "Email already exists")]
        public string Email { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Full name should begin with letter or number")]
        public string Fullname { get; set; }
        public Tutor Add(string userId)
        {
            return new Tutor
            {
                Email = Email,
                Fullname = Fullname,
                UserId = userId,
            };
        }
    }

    public class EditTutorModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Full name should begin with letter or number")]
        public string Fullname { get; set; }
        public EditTutorModel()
        {

        }
        public EditTutorModel(Tutor tutor)
        {
            Id = tutor.Id;
            Email = tutor.Email;
            Fullname = tutor.Fullname;
        }
        public Tutor Edit(Tutor tutor)
        {
            tutor.Fullname = Fullname;
            return tutor;
        }
    }



}
