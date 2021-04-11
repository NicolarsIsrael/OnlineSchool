using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Core;
using OnlineSchool.Service.Contract;
using OnlineSchool.Utility;

namespace OnlineSchool.Controllers
{
    public class BaseController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly RoleManager<ApplicationRole> _roleManager;
        public readonly IStudentService _studentService;
        public readonly ILectureService _lectureService;
        public readonly ITutorService _tutorService;
        public readonly ICourseService _courseService;
        public readonly IExamService _examService;
        public readonly IEmailService _emailSender;

        public BaseController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService, IExamService examService, IEmailService emailSender)
        {
            _userManager = userManager;
            _studentService = studentService;
            _roleManager = roleManager;
            _tutorService = tutorService;
            _courseService = courseService;
            _examService = examService;
            _emailSender = emailSender;
            _lectureService = lectureService;
        }

        public ApplicationUser GetLoggedInUser(bool allowNull = false)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (!allowNull && user == null)
                throw new Exception();
            return user;
        }

        public Tutor GetLoggedInTutor()
        {
            var userId = GetLoggedInUser().Id;
            var tutor = _tutorService.GetByUserId(userId);
            return tutor;
        }

        public Student GetLoggedInStudent()
        {
            var userId = GetLoggedInUser().Id;
            var student = _studentService.GetByUserId(userId);
            return student;
        }
    }
}
