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
        private IExamAttemptService _examAttemptService;
        public CourseController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService, IExamService examService, IEmailService emailSender, IExamAttemptService examAttemptService)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
            _examAttemptService = examAttemptService;
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


        [Authorize(Roles = AppConstant.StudentRole)]
        public IActionResult Result(int id)
        {
            var studentId = GetLoggedInStudent().Id;
            var studentAttempt = _examAttemptService.GetAllExamAttempts(id).Where(s => s.StudentId ==studentId).FirstOrDefault();

            if (studentAttempt == null)
                return View(null);


            if (!studentAttempt.IsGrraded)
                return View(null);

            var examModel = new ExamModel(_examService.Get(studentAttempt.ExamId), studentAttempt, 1, true);
            var model = new AttemptResultModel(new ExamAttemptModel(studentAttempt), examModel);
            return View(model);
        }

    }
}
