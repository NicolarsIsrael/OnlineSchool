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
    public class CourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly ILectureService _lectureService;
        public CourseController(UserManager<ApplicationUser> userManager, IStudentService studentService, ICourseService courseService, ILectureService lectureService)
        {
            _userManager = userManager;
            _studentService = studentService;
            _courseService = courseService;
            _lectureService = lectureService;
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
            var model = new ViewCourseDetailsModel(course, lectures);
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

        private ApplicationUser GetLoggedInUser(bool allowNull=false)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (!allowNull && user == null)
                throw new Exception();
            return user;
        }

        private Student GetLoggedInStudent()
        {
            var userId = GetLoggedInUser().Id;
            var student = _studentService.GetByUserId(userId);
            return student;
        }
    }
}
