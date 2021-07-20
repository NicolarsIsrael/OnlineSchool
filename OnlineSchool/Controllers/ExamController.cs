using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Core;
using OnlineSchool.Models;
using OnlineSchool.Service.Contract;
using OnlineSchool.Utility;

namespace OnlineSchool.Controllers
{
    public class ExamController : BaseController
    {
        private readonly IExamAttemptService _examAttemptService;
        private readonly IExamMcqAttemptService _examMcqAttemptService;
        public ExamController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService, IExamService examService, IExamAttemptService examAttemptService, IExamMcqAttemptService examMcqAttemptService, IEmailService emailSender)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
            _examAttemptService = examAttemptService;
            _examMcqAttemptService = examMcqAttemptService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var exam = _examService.Get(id);
            var student = GetLoggedInStudent();
            var examAttempt = _examAttemptService.CheckIfStudentAttemptAlreadyExists(exam.Id, student.Id);
            if (examAttempt == null)
            {
                var mcqAttempts = new List<ExamMcqAttempt>();
                foreach(var mcq in exam.MultiChoiceQuestions)
                {
                    mcqAttempts.Add(new ExamMcqAttempt()
                    {
                        DateCreated = DateTime.Now,
                        DateCreatedUtc = DateTime.UtcNow,
                        DateModified = DateTime.Now,
                        DateModifiedUtc = DateTime.UtcNow,
                        McqId = mcq.Id,
                        SelectedOptionId = -1,
                        McqOptions = mcq.Options,
                    });
                }
                examAttempt = new ExamAttempt()
                {
                    CourseId = exam.Course.Id,
                    StudentId = student.Id,
                    ExamId = exam.Id,
                    Mcqs = mcqAttempts,
                    DurationInSeconds = exam.DurationInMinute * 60,
                };
                examAttempt = await _examAttemptService.CreateExamAttempt(examAttempt);
            }

            //examAttempt.DurationInSeconds = 20;
            //await _examAttemptService.Update(examAttempt);

            var model = new ExamModel(exam,examAttempt);
            return View(model);
        }

        public async Task<IActionResult> SubmitMcqAnswer(int mcqAttemptId, int answerId)
        {
            var mcqAnswer = _examMcqAttemptService.Get(mcqAttemptId);
            mcqAnswer.SelectedOptionId = answerId;
            await _examMcqAttemptService.Update(mcqAnswer);
            return Json(new { });
        }

        public async Task<IActionResult> UpdateDuration(int examAttemptId,int durationLeft)
        {
            var examAttempt = _examAttemptService.Get(examAttemptId);
            examAttempt.DurationInSeconds = durationLeft;
            if (examAttempt.ContinueAttempt)
                await _examAttemptService.Update(examAttempt);
            if(examAttempt.DurationInSeconds== 0)
            {
                examAttempt.ContinueAttempt = false;
                await _examAttemptService.Update(examAttempt);
            }
            return Json(new { });
        }

        public async Task<IActionResult> AttemptFinished(int id)
        {
            var examAttempt = _examAttemptService.Get(id);
            examAttempt.ContinueAttempt = false;
            await _examAttemptService.Update(examAttempt);
            return Content("Done");
        }

        [HttpPost]
        public IActionResult Index(ExamModel model)
        {
            return View();
        }


    }
}
