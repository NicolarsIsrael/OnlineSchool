using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Core;
using OnlineSchool.Models;
using OnlineSchool.Service.Contract;
using OnlineSchool.Utility;

namespace OnlineSchool.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService, IExamService examService, IEmailService emailSender)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
        }

        [Authorize(Roles =AppConstant.StudentRole)]
        public IActionResult Index()
        {
            var student = GetLoggedInStudent();
            var model = student.Courses.Select(c => new ViewCourseModel(c));
            return View(model);
        }

        public IActionResult View(int id)
        {
            var student = GetLoggedInStudent();
            var course = _courseService.Get(id);
            if (!student.Courses.Contains(course))
                return NotFound();
            var lectures = _lectureService.GetAllForCourse(course.Id);
            var exams = _examService.GetForCourse(course.Id).ToList();
            var model = new ViewCourseDetailsModel(course, lectures, exams.Select(e=> new ViewExamModel(e)));
            return View(model);
        }

        [Authorize(Roles = AppConstant.StudentRole)]
        public IActionResult Registeration()
        {
            var student = GetLoggedInStudent();
            var courses = _courseService.GetAll();
            var _courses = courses.Select(c => new CourseRegisterationViewModel(c, _courseService.CheckIfStudentOfferCourse(student, c)));
            var model = new CourseRegModel(_courses.ToList());
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AppConstant.StudentRole)]
        public async Task<IActionResult> Registeration(CourseRegModel model)
        {
            var student = GetLoggedInStudent();
            var registeredCourses = new List<Course>();
            foreach (var course in model.Courses.Where(c => c.IsSelected))
                registeredCourses.Add(_courseService.Get(course.Id));
            student.Courses = registeredCourses;
            await _studentService.Update(student);
            return RedirectToAction(nameof(Index));
        }

    }
}
