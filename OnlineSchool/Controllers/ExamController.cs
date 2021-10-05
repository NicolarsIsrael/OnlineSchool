﻿using System;
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
    public class ExamController : BaseController
    {
        private readonly IExamAttemptService _examAttemptService;
        private readonly IExamMcqAttemptService _examMcqAttemptService;
        private readonly IExamTheoryAttemptService _examTheoryAttemptService;

        public ExamController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStudentService studentService, ILectureService lectureService, ITutorService tutorService, ICourseService courseService, IExamService examService, IExamAttemptService examAttemptService, IExamMcqAttemptService examMcqAttemptService, IEmailService emailSender, IExamTheoryAttemptService examTheoryAttemptService)
            : base(userManager, roleManager, studentService, lectureService, tutorService, courseService, examService, emailSender)
        {
            _examAttemptService = examAttemptService;
            _examMcqAttemptService = examMcqAttemptService;
            _examTheoryAttemptService = examTheoryAttemptService;
        }

        [Route("exam/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var exam = _examService.Get(id);
            var student = GetLoggedInStudent();

            ExamAttempt examAttempt =  _examAttemptService.CheckIfStudentAttemptAlreadyExists(exam.Id, student.Id);
            if (examAttempt == null)
            {
                var totalNumberOfQuestions = exam.MultiChoiceQuestions.Count() + exam.TheoryQuestions.Count();
                var mcqAttempts = GenerateMcqQuestions(exam);
                var theoryAttempts = GenerateTheoryQuestions(exam);

                examAttempt = new ExamAttempt()
                {
                    CourseId = exam.Course.Id,
                    StudentId = student.Id,
                    Student = student,
                    ExamId = exam.Id,
                    Mcqs = mcqAttempts,
                    Theorys = theoryAttempts,
                    DurationInSeconds = exam.DurationInMinute * 60,
                    ContinueAttempt = true,
                    MaximumScore = exam.TotalScore,
                    IsGrraded = false,
                };
                examAttempt = await _examAttemptService.CreateExamAttempt(examAttempt);
            }
            if (!CheckIfAttemptIsAllowed(exam, examAttempt))
                return RedirectToAction(nameof(AttemptFinished), new { id = exam.Id });

            var model = new ExamModel(exam,examAttempt,1);
            return View(model);
        }

        private IEnumerable<ExamMcqAttempt> GenerateMcqQuestions(Exam exam)
        {
            var mcqAttempts = new List<ExamMcqAttempt>();
            var questionNumbers = Enumerable.Range(1, exam.MultiChoiceQuestions.Count()).ToList();
            foreach (var mcq in exam.MultiChoiceQuestions)
            {
                var questionNumberIndex = new Random().Next(0, questionNumbers.Count());
                var questionNumber = questionNumbers[questionNumberIndex];
                questionNumbers.Remove(questionNumber);

                mcqAttempts.Add(new ExamMcqAttempt()
                {
                    DateCreated = DateTime.Now,
                    DateCreatedUtc = DateTime.UtcNow,
                    DateModified = DateTime.Now,
                    DateModifiedUtc = DateTime.UtcNow,
                    McqId = mcq.Id,
                    SelectedOptionId = -1,
                    CorrectAnswerId = mcq.AnswerId,
                    Score = mcq.Score,
                    McqOptions = mcq.Options,
                    QuestionNumber = questionNumber,
                    PageNumber = questionNumber % AppConstant.NumberOfQuestionsPerPage == 0
                                ? questionNumber / AppConstant.NumberOfQuestionsPerPage
                                : (questionNumber / AppConstant.NumberOfQuestionsPerPage) + 1,
                });
            }
            return mcqAttempts;
        }

        private IEnumerable<ExamTheoryAttempt> GenerateTheoryQuestions(Exam exam)
        {
            var theoryAttemps = new List<ExamTheoryAttempt>();
            var questionNumbers = Enumerable.Range(exam.MultiChoiceQuestions.Count()+1, exam.TheoryQuestions.Count()).ToList();

            foreach(var theory in exam.TheoryQuestions)
            {
                var questionNumberIndex = new Random().Next(0, questionNumbers.Count());
                var questionNumber = questionNumbers[questionNumberIndex];
                questionNumbers.Remove(questionNumber);

                theoryAttemps.Add(new ExamTheoryAttempt()
                {
                    DateCreated = DateTime.Now,
                    DateCreatedUtc = DateTime.UtcNow,
                    DateModified = DateTime.Now,
                    DateModifiedUtc = DateTime.UtcNow,
                    TheoryQuestionId = theory.Id,
                    IsDeleted = false,
                    Answer = "",
                    QuestionNumber = questionNumber,
                    PageNumber = questionNumber % AppConstant.NumberOfQuestionsPerPage == 0
                                ? questionNumber / AppConstant.NumberOfQuestionsPerPage
                                : (questionNumber / AppConstant.NumberOfQuestionsPerPage) + 1,
                });
                
            }
            return theoryAttemps;
        }


        public IActionResult movetopage(int attemptId, int pageNumber)
        {
            try
            {
                var attempt = _examAttemptService.Get(attemptId);
                var exam = _examService.Get(attempt.ExamId);
                var model = new ExamModel(exam, attempt, pageNumber);
                return PartialView("_QuestionsList", model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IActionResult> SubmitMcqAnswer(int mcqAttemptId, int answerId)
        {
            var mcqAnswer = _examMcqAttemptService.Get(mcqAttemptId);
            mcqAnswer.SelectedOptionId = answerId;
            await _examMcqAttemptService.Update(mcqAnswer);
            return Json(new { });
        }

        public async Task<IActionResult> SubmitTheoryAnswer(int theoryAttemptId, string answer)
        {
            var theoryAttempt = _examTheoryAttemptService.Get(theoryAttemptId);
            theoryAttempt.Answer = answer;
            await _examTheoryAttemptService.Updte(theoryAttempt);
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
            examAttempt.ContinueAttempt = true;
            examAttempt.Score = CalculateScore(examAttempt);

            examAttempt.IsGrraded = examAttempt.Theorys.Count() > 0 ? false : true;
            await _examAttemptService.Update(examAttempt);
            var exam = _examService.Get(examAttempt.ExamId);
            var model = new AttemptCompletedModel(exam, examAttempt);
            return View(model);
        }


        [HttpPost]
        public IActionResult Index(ExamModel model)
        {
            return View();
        }

        public bool CheckIfAttemptIsAllowed(Exam exam, ExamAttempt attempt)
        {
            if (!attempt.ContinueAttempt)
                return false;

            //if (DateTime.Compare(exam.StartTime, DateTime.Now) > 0)
            //    return false;

            //if (DateTime.Compare(exam.DeadlineEndTime, DateTime.Now) < 0)
            //    return false;

            return true;
        }


        //[Route("grade-attempt/{id}")]
        [Authorize(Roles =AppConstant.LecturerRole)]
        public IActionResult GradeAttempt(int id)
        {
            var examAttempt = _examAttemptService.Get(id);
            if (examAttempt == null)
                return NotFound();
            var examModel = new ExamModel(_examService.Get(examAttempt.ExamId), examAttempt, 1, true);
            var model = new AttemptResultModel(new ExamAttemptModel(examAttempt), examModel);
            return View(model);
        }

        [Authorize(Roles=AppConstant.LecturerRole)]
        [HttpPost]
        public async Task<IActionResult> GradeAttempt(AttemptResultModel model)
        {
            var examAttempt = _examAttemptService.Get(model.AttemptId);
            foreach(var theory in model.Exam.TheoryQuestions.ToList())
            {
                var theoryAttempt = _examTheoryAttemptService.Get(theory.AttemptId);
                theoryAttempt.StudentScore = theory.StudentScore;
                await _examTheoryAttemptService.Updte(theoryAttempt);
            }
            examAttempt.Score = CalculateScore(examAttempt);
            examAttempt.IsGrraded = true;
            await _examAttemptService.Update(examAttempt);

            return RedirectToAction("Result", "tutor", new { id = examAttempt.ExamId });
        }

        public async Task<IActionResult> Invigilate(int id)
        {
            var username = GetLoggedInUser().UserName;

            ViewBag.Room = "";
            ViewBag.Topic = "";
            ViewBag.Username = username;
            ViewBag.Token = GeneralFunction.GenerateVideoMeetingToken(username);
            return View();
        }

        private decimal CalculateScore(ExamAttempt examAttempt)
        {
            decimal score = 0;
            foreach (var mcq in examAttempt.Mcqs)
            {
                if (mcq.SelectedOptionId == mcq.CorrectAnswerId)
                    score += mcq.Score;
            }
            score += examAttempt.Theorys.Sum(t => t.StudentScore);
            return score;
        }
    }
}
