using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineSchool.Core;
using OnlineSchool.Models;
using OnlineSchool.Service.Contract;
using OnlineSchool.Utility;

namespace OnlineSchool.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILectureService _service;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService, IExamService examService, IEmailService emailSender)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
        }
        public IActionResult Index()
        {
            //return Content(_service.GetCount().ToString());
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
