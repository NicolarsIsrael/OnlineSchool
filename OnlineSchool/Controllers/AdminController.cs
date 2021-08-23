using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Core;
using OnlineSchool.Models;
using OnlineSchool.Service.Contract;
using OnlineSchool.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool.Controllers
{
    [Authorize(Roles =AppConstant.SuperAdminRole)]
    public class AdminController : BaseController
    {
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService, IExamService examService, IEmailService emailSender)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
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
            var password = model.LastName;// GeneralFunction.GeneratePassword();
            var user = await CreateUserAccount(AppConstant.StudentRole, model.Email,password);
            try
            {
                var url = "wwwroot/default-profile-image.jpg";
                if (model.ProfileImage != null)
                    url = await FileService.SaveDoc(model.ProfileImage, "ProfileImages", FileService.FileType.Image);
                var student = model.Add(user.Id, url);
                await _studentService.Add(student);
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
            //try
            //{
            //    await _emailSender.SendEmailAsync(model.Email,
            //           "Student account",
            //                    $"A student account has been created for you with the following credentials." +
            //                    $"<br>Email: {model.Email}<br>Password: {password}" +
            //                    $"<br>Login and change password to a more familiar one.");
            //}
            //catch (Exception ex)
            //{ }
            return RedirectToAction(nameof(Students));
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
            var password = model.Fullname; // GeneralFunction.GeneratePassword();
            var user = await CreateUserAccount(AppConstant.LecturerRole, model.Email, password);
            try
            {
                var tutor = model.Add(user.Id);
                await _tutorService.Add(tutor);
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
            //try
            //{
            //    await _emailSender.SendEmailAsync(model.Email,
            //           "Lecturer account",
            //                    $"A lecturer account has been created for you with the following credentials." +
            //                    $"<br>Email: {model.Email}<br>Password: {password}" +
            //                    $"<br>Login and change your password to a more familiar one");
            //}
            //catch (Exception ex)
            //{ }
            return RedirectToAction(nameof(Tutors));
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

        public IActionResult Courses()
        {
            var courses = _courseService.GetAll();
            var model = courses.Select(c => new ViewCourseModel(c)).OrderBy(c => c.CourseCode);
            return View(model);
        }

        public IActionResult AddCourse()
        {
            var tutors = _tutorService.GetAll();
            var model = new AddCourseViewModel(tutors);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var tutor = _tutorService.Get(model.SelectedTutor);
            var course = model.Add(tutor);
            await _courseService.Add(course);
            return RedirectToAction(nameof(Courses));
        }

        public IActionResult EditCourse(int id)
        {
            var course = _courseService.Get(id);
            var tutors = _tutorService.GetAll();
            var model = new EditCourseViewModel(course, tutors);
            TempData["CurrentCourseCode"] = course.CourseCode;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(EditCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var course = _courseService.Get(model.Id);
            var tutor = _tutorService.Get(model.SelectedTutor);
            course = model.Edit(course,tutor);
            await _courseService.Update(course);
            return RedirectToAction(nameof(Courses));
        }

        private async Task<ApplicationUser> CreateUserAccount(string userRole, string email,string password)
        {

            if (await _roleManager.FindByNameAsync(userRole) == null)
                await _roleManager.CreateAsync(new ApplicationRole(userRole));

            var user = new ApplicationUser() { Email = email, UserName = email };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception();
            await _userManager.AddToRoleAsync(user, userRole);
            return user;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            var user =await _userManager.FindByNameAsync(email);
            if (user == null)
                return true;

            return false;
        }

        public bool UniqueCourseCode(string courseCode)
        {
            var course = _courseService.GetByCourseCode(courseCode);
            if (course == null)
                return true;
            return false;
        }

        public bool UniqueCourseCodeEdit(string courseCode)
        {
            string currentCourseCode = TempData["CurrentCourseCode"].ToString();
            if (string.Compare(courseCode, currentCourseCode, true) == 0)
            {
                TempData["CurrentCourseCode"] = currentCourseCode;
                return true;
            }
            var course = _courseService.GetByCourseCode(courseCode);
            if (course == null)
            {
                TempData["CurrentCourseCode"] = currentCourseCode;
                return true;
            }
            TempData["CurrentCourseCode"] = currentCourseCode;
            return false;

        }
    }
}
