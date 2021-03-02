using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles =AppConstant.SuperAdminRole)]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IStudentService _studentService;
        private readonly ITutorService _tutorService;
        public AdminController(UserManager<ApplicationUser> userManager, IStudentService studentService, RoleManager<ApplicationRole> roleManager,ITutorService tutorService)
        {
            _userManager = userManager;
            _studentService = studentService;
            _roleManager = roleManager;
            _tutorService = tutorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Students()
        {
            var students = _studentService.GetAll();
            var model = students.Select(s => new ViewStudentsModel(s)).OrderBy(s => s.Email);
            return View(model);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var user = await CreateUserAccount(AppConstant.StudentRole, model.Email);
            try
            {
                var url = "wwwroot/default-profile-image.jpg";
                if (model.ProfileImage != null)
                    url = await FileService.SaveDoc(model.ProfileImage, "ProfileImages", FileService.FileType.Image);
                var student = model.Add(user.Id, url);
                await _studentService.Add(student);
                return RedirectToAction(nameof(Students));
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }

        public IActionResult EditStudent(int id)
        {
            var student = _studentService.Get(id);
            var model = new EditStudentModel(student);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(EditStudentModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var student = _studentService.Get(model.Id);
            var url = student.ProfilePicturePath;
            if (model.ProfileImage != null)
                url = await FileService.SaveDoc(model.ProfileImage, "ProfileImages", FileService.FileType.Image);
            student = model.Edit(student, url);
            await _studentService.Update(student);
            return RedirectToAction(nameof(Students));
        }

        public IActionResult Tutors()
        {
            var tutors = _tutorService.GetAll();
            var model = tutors.Select(t => new ViewTutorModel(t)).OrderBy(t => t.Email);
            return View(model);
        }

        public IActionResult AddTutor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTutor(AddTutorModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var user = await CreateUserAccount(AppConstant.LecturerRole, model.Email);
            try
            {
                var tutor = model.Add(user.Id);
                await _tutorService.Add(tutor);
                return RedirectToAction(nameof(Tutors));
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }

        public IActionResult EditTutor(int id)
        {
            var tutor = _tutorService.Get(id);
            var model = new EditTutorModel(tutor);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTutor(EditTutorModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }

            var tutor = _tutorService.Get(model.Id);
            tutor = model.Edit(tutor);
            await _tutorService.Update(tutor);
            return RedirectToAction(nameof(Tutors));
        }

        private async Task<ApplicationUser> CreateUserAccount(string userRole, string email)
        {

            if (await _roleManager.FindByNameAsync(userRole) == null)
                await _roleManager.CreateAsync(new ApplicationRole(userRole));

            var user = new ApplicationUser() { Email = email, UserName = email };
            var result = await _userManager.CreateAsync(user, GeneralFunction.GeneratePassword());
            if (!result.Succeeded)
                throw new Exception();
            await _userManager.AddToRoleAsync(user, AppConstant.SuperAdminRole);
            return user;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            var user =await _userManager.FindByNameAsync(email);
            if (user == null)
                return true;

            return false;
        }

    }
}
