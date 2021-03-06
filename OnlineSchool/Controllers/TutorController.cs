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
using OnlineSchool.Utility;

namespace OnlineSchool.Controllers
{
    public class TutorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICourseService _courseService;
        private readonly ILectureService _lectureService;
        private readonly ITutorService _tutorService;
        public TutorController(ICourseService courseService, ILectureService lectureService, ITutorService tutorService, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _lectureService = lectureService;
            _tutorService = tutorService;
            _userManager = userManager;
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
            var model = new ViewCourseDetailsModel(course, lectures);
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


        private ApplicationUser GetLoggedInUser(bool allowNull = false)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (!allowNull && user == null)
                throw new Exception();
            return user;
        }

        private Tutor GetLoggedInTutor()
        {
            var userId = GetLoggedInUser().Id;
            var tutor = _tutorService.GetByUserId(userId);
            return tutor;
        }
    }
}
