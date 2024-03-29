﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Models
{
    public class ViewCourseModel
    {
        public int Id { get; set; }
        [Display(Name ="Course title")]
        public string CourseTitle { get; set; }
        [Display(Name = "Course code")]
        public string CourseCode { get; set; }
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        [Display(Name = "Tutor")]
        public string TutorName { get; set; }
        public ViewCourseModel(Course course)
        {
            Id = course.Id;
            CourseTitle = course.CourseTitle;
            CourseCode = course.CourseCode;
            CourseDescription = course.Description;
            TutorName = course.Tutor.Fullname;
        }
    }

    public class ViewCourseDetailsModel
    {
        public int Id { get; set; }
        [Display(Name = "Course title")]
        public string CourseTitle { get; set; }
        [Display(Name = "Course code")]
        public string CourseCode { get; set; }
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        [Display(Name = "Tutor")]
        public string TutorName { get; set; }
        public int NumberOfLectures { get; set; }
        public int NumberOfExams { get; set; }
        public IEnumerable<ViewLectureModel> Lectures { get; set; }
        public IEnumerable<ViewExamModel> Exams { get; set; }
        public ViewCourseDetailsModel(Course course, IEnumerable<Lecture> lectures)
        {
            Id = course.Id;
            CourseTitle = course.CourseTitle;
            CourseCode = course.CourseCode;
            CourseDescription = course.Description;
            TutorName = course.Tutor.Fullname;
            Lectures = lectures.Select(l => new ViewLectureModel(l));
            NumberOfLectures = lectures.Count();
        }

        public ViewCourseDetailsModel(Course course,IEnumerable<Lecture> lectures, IEnumerable<ViewExamModel> exams)
        {
            Id = course.Id;
            CourseTitle = course.CourseTitle;
            CourseCode = course.CourseCode;
            CourseDescription = course.Description;
            TutorName = course.Tutor.Fullname;
            Lectures = lectures.Select(l => new ViewLectureModel(l));
            NumberOfLectures = lectures.Count();
            Exams = exams;
            NumberOfExams = exams.Count();
        }
    }

    public class AddCourseViewModel
    {
        [Required(ErrorMessage ="Course code is required")]
        [Display(Name = "Course title")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Course should begin with letter or number")]
        public string CourseTitle { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [Display(Name = "Course code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid course code")]
        [Remote("UniqueCourseCode", "Admin", ErrorMessage = "Course code already exists")]
        public string CourseCode { get; set; }

        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        public int SelectedTutor { get; set; }
        public List<SelectListItem> Tutors { get; set; }
        public AddCourseViewModel(IEnumerable<Tutor> tutors)
        {
            tutors = tutors.OrderBy(t => t.Fullname);
            var tutorsList = new List<SelectListItem>();
            foreach (var t in tutors)
                tutorsList.Add(new SelectListItem(t.Fullname, t.Id.ToString()));
            Tutors = tutorsList;
        }

        public AddCourseViewModel()
        {

        }
        public Course Add(Tutor tutor)
        {
            return new Course
            {
                CourseCode = CourseCode,
                CourseTitle = CourseTitle,
                Description = CourseDescription,
                Tutor = tutor,
            };
        }
    }

    public class EditCourseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [Display(Name = "Course title")]
        [RegularExpression("^[a-zA-Z0-9].*$", ErrorMessage = "Course should begin with letter or number")]
        public string CourseTitle { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [Display(Name = "Course code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid course code")]
        [Remote("UniqueCourseCodeEdit", "Admin", ErrorMessage = "Course code already exists")]
        public string CourseCode { get; set; }

        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        public int SelectedTutor { get; set; }
        public List<SelectListItem> Tutors { get; set; }
        public EditCourseViewModel(Course course, IEnumerable<Tutor> tutors)
        {
            tutors = tutors.OrderBy(t => t.Fullname);
            var tutorsList = new List<SelectListItem>();
            var selectedTutor = tutors.Where(t => t.Id == course.TutorId).First();
            tutorsList.Add(new SelectListItem(selectedTutor.Fullname, selectedTutor.Id.ToString(), true));
            foreach (var t in tutors.Where(tutor=>tutor!=selectedTutor))
                tutorsList.Add(new SelectListItem() { Text = t.Fullname, Value = t.Id.ToString(), Disabled = false });
            Tutors = tutorsList;
            Id = course.Id;
            CourseCode = course.CourseCode;
            CourseTitle = course.CourseTitle;
            CourseDescription = course.Description;
        }

        public EditCourseViewModel()
        {

        }
        public Course Edit(Course course,Tutor tutor)
        {
            course.CourseTitle = CourseTitle;
            course.CourseCode = CourseCode;
            course.Description = CourseDescription;
            course.Tutor = tutor;
            return course;
        }
    }

    public class CourseRegModel
    {
        public List<CourseRegisterationViewModel> Courses { get; set; }
        public CourseRegModel() { }
        public CourseRegModel(List<CourseRegisterationViewModel> courses)
        {
            Courses = courses;
        }
    }

    public class CourseRegisterationViewModel
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public bool IsSelected { get; set; }
        public CourseRegisterationViewModel() { }
        public CourseRegisterationViewModel(Course course, bool isSelected)
        {
            Id = course.Id;
            CourseCode = course.CourseCode;
            CourseTitle = course.CourseTitle;
            IsSelected = isSelected;
        }
    }
}
