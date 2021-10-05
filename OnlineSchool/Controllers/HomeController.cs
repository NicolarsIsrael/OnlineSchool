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
using Twilio.Rest.Video.V1;
using Twilio;
using XSockets.Core.Common.Socket;
using Twilio.Jwt.AccessToken;

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
        public async Task<IActionResult> Index()
        {
            //return View();
            var user = GetLoggedInUser(true);
            if (user == null)
                return RedirectToPage("/Account/Login",new { area = "Identity"});
            if (await _userManager.IsInRoleAsync(user, AppConstant.StudentRole))
                return RedirectToAction("Index", "Course");

            if(await _userManager.IsInRoleAsync(user, AppConstant.LecturerRole))
                return RedirectToAction("Index", "Tutor");

            if (await _userManager.IsInRoleAsync(user, AppConstant.SuperAdminRole))
                return RedirectToAction("Index", "Admin");

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Chat(string room,string username)
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
