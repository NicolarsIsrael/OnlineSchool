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
        public AdminController(UserManager<ApplicationUser> userManager, IStudentService studentService, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _studentService = studentService;
            _roleManager = roleManager;
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
