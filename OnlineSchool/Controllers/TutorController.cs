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
        public readonly IMcqQuestionService _mcqQuestionService;
        public readonly IMcqOptionService _mcqOptionService;
        public readonly IExamAttemptService _examAttemptService;

        public TutorController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService,IExamService examService, IEmailService emailSender, IMcqQuestionService mcqQuestionService,
            IMcqOptionService mcqOptionService, IExamAttemptService examAttemptService)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
            _mcqQuestionService = mcqQuestionService;
            _mcqOptionService = mcqOptionService;
            _examAttemptService = examAttemptService;
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

        public IActionResult Result(int id)
        {
            var tutor = GetLoggedInTutor();
            var exam = _examService.Get(id);
            if (tutor.Id != exam.Course.TutorId)
                return NotFound();
            var examAttempts = _examAttemptService.GetAllExamAttempts(id);
            var model = examAttempts.Select(ex => new ExamAttemptModel { StudentFullname = $"{ex.Student.FirstName} {ex.Student.LastName}", StudentId = ex.StudentId,
                StudentMatricNumber = ex.Student.MatricNumber, StudentScore = ex.Score, TotalGrade = ex.MaximumScore });
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

        public IActionResult Exam(int id)
        {
            var exam = _examService.Get(id);
            if (exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();
            var model = new ViewExamModel(exam);
            return View(model);
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
            return RedirectToAction(nameof(Exam), new { id = exam.Id });
        }

        public IActionResult EditExam(int id)
        {
            var exam = _examService.Get(id);
            if (exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();
            return View(new EditExamModel(exam));
        }

        [HttpPost]
        public async Task<IActionResult> EditExam(EditExamModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var exam = _examService.Get(model.ExamId);
            if (exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();

            exam = model.Edit(exam);
            await _examService.Update(exam);
            return RedirectToAction(nameof(Course), new { id = model.CourseId });
        }

        public IActionResult AddMcq(int id)
        {
            var exam = _examService.Get(id);
            if (exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();

            return View(new AddMcqModel(exam));
        }

        [HttpPost]
        public async Task<IActionResult> AddMcq(AddMcqModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }
            var exam = _examService.Get(model.ExamId);
            if (exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();

            var options = new List<McqOption>();
            foreach (var option in model.Options)
                options.Add(option.Add());
            await _mcqQuestionService.Add(model.Add(options, exam));
            return RedirectToAction(nameof(Exam), new { id = model.ExamId });
        }

        //public async Task<IActionResult> addMcqData(int id)
        //{
        //    var exam = _examService.Get(id);
        //    var mcq = new List<McqQuestion>()
        //    {
        //        new McqQuestion{Exam = exam, Question = "adfhalsdfkj adfsdkjsld",Options = new List<McqOption>{new McqOption { Option = "abd"} }}
        //    }
        //}

        public IActionResult EditMcq(int id)
        {
            var mcq = _mcqQuestionService.GetById(id);
            if (mcq.Exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();

            var model = new EditMcqModel(mcq);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditMcq(EditMcqModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(model);
            }

            var mcq = _mcqQuestionService.GetById(model.Id);
            if (mcq.Exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();
            var exam = _examService.Get(mcq.ExamId);

            foreach (var opt in model.Options)
            {
                var option = _mcqOptionService.Get(opt.Id);
                option = opt.Edit(option);
                await _mcqOptionService.Update(option);
            }
            mcq = model.Edit(mcq);
            await _mcqQuestionService.Update(mcq,exam);

            return RedirectToAction(nameof(Exam), new { id = mcq.ExamId });
        }

        public async Task<IActionResult> DeleteMcq(int id)
        {
            var mcq = _mcqQuestionService.GetById(id);
            if (mcq.Exam.Course.TutorId != GetLoggedInTutor().Id)
                return NotFound();

            await _mcqQuestionService.Delete(mcq);

            return RedirectToAction(nameof(Exam), new { id = mcq.ExamId });
        }

        //public async Task<IActionResult> GradeExam(int id)
        //{
        //    var exam = _examService.Get(id);
        //    var examAttempts = _examAttemptService.GetAllExamAttempts(id);
        //    foreach(var examAttempt in examAttempts)
        //    {
        //        foreach(var mcq in examAttempt.Mcqs)
        //        {
        //            if(mcq.SelectedOptionId==mcq.)
        //        }
        //    }
        //}
    }
}
