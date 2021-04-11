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
    [Authorize(Roles =AppConstant.LecturerRole)]
    public class TutorController : BaseController
    {
        public TutorController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService,IExamService examService, IEmailService emailSender)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Courses()
        {
            var tutor = GetLoggedInTutor();
            var courses = _courseService.GetAllForTutor(tutor.Id);
            var model = courses.Select(c => new ViewCourseModel(c));
            return View(model);
        }

        public IActionResult Course(int id)
        {
            var tutor = GetLoggedInTutor();
            var course = _courseService.GetForTutor(id, tutor.Id);
            var lectures = _lectureService.GetAllForCourse(course.Id);
            var exams = _examService.GetForCourse(id).Select(e => new ViewExamModel(e));
            var model = new ViewCourseDetailsModel(course, lectures,exams);
            return View(model);
        }

        public IActionResult NewLecture(int id)
        {
            var course = _courseService.Get(id);
            return View(new AddLectureModel(course.Id));
        }

        [HttpPost]
        public async Task<IActionResult> NewLecture(AddLectureModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var course = _courseService.Get(model.CourseId);
            var url = "";
            if(model.File!=null)
                url = await FileService.SaveDoc(model.File, $"Lectures/{course.Id}", FileService.FileType.Image);
            var lecture = model.Add(url, course);
            await _lectureService.Add(lecture);
            return RedirectToAction(nameof(Course), new { id = lecture.CourseId });
        }

        public IActionResult NewExam(int id)
        {
            var course = _courseService.Get(id);
            return View(new NewExamModel(course.Id));
        }

        [HttpPost]
        public async Task<IActionResult> NewExam(NewExamModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var course = _courseService.Get(model.CourseId);
            var exam = model.Add(course);
            await _examService.Add(exam);
            return RedirectToAction(nameof(Course), new { id = model.CourseId });
        }
    }
}
