using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Core;
using OnlineSchool.Models;
using OnlineSchool.Service.Contract;

namespace OnlineSchool.Controllers
{
    public class CourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        public CourseController(UserManager<ApplicationUser> userManager, IStudentService studentService, ICourseService courseService)
        {
            _userManager = userManager;
            _studentService = studentService;
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            var student = GetLoggedInStudent();
            var model = student.Courses.Select(c => new ViewCourseModel(c));
            return View(model);
        }

        public IActionResult Registeration()
        {
            var student = GetLoggedInStudent();
            var courses = _courseService.GetAll();
            var _courses = courses.Select(c => new CourseRegisterationViewModel(c, _courseService.CheckIfStudentOfferCourse(student, c)));
            var model = new CourseRegModel(_courses.ToList());
            return View(model);
        }

        [HttpPost]
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
