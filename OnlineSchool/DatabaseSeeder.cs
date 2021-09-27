using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineSchool.Core;
using OnlineSchool.Data;
using OnlineSchool.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSchool
{
    public class DatabaseSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var _roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                using (var context = new ApplicationDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {

                    if (await _roleManager.FindByNameAsync(AppConstant.SuperAdminRole) == null)
                    {
                        string superAdminEmail = Startup.StaticConfig.GetValue<string>("AppSettings:SuperAdminEmail");
                        string superAdminPassword = Startup.StaticConfig.GetValue<string>("AppSettings:SuperAdminPassword");

                        await _roleManager.CreateAsync(new ApplicationRole(AppConstant.SuperAdminRole));
                        var user = new ApplicationUser { UserName = superAdminEmail, Email = superAdminEmail};
                        var result = await _userManager.CreateAsync(user, superAdminPassword);
                        if (!result.Succeeded)
                            throw new Exception();
                        await _userManager.AddToRoleAsync(user, AppConstant.SuperAdminRole);

                    }

                    if (await _roleManager.FindByNameAsync(AppConstant.LecturerRole) == null)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(AppConstant.LecturerRole));

                        string userName = "lanreogunjide@gmail.com";
                        string password = "lanre";
                        var tutorUser1 = new ApplicationUser { UserName = userName, Email = userName };
                        var result = await _userManager.CreateAsync(tutorUser1, password);
                        await _userManager.AddToRoleAsync(tutorUser1, AppConstant.LecturerRole);

                        var tutor1 = new Tutor { UserId = tutorUser1.Id, Email = tutorUser1.Email, Fullname = "Dr. Lanre Ogunjide" };

                        var course1 = new Course
                        {
                            CourseCode = "GST 203",
                            CourseTitle = "General knowledge",
                            DateCreated = DateTime.Now,
                            DateCreatedUtc = DateTime.Now,
                            DateModified = DateTime.Now,
                            DateModifiedUtc = DateTime.Now,
                            Description = "Test your knowledge on general knowledge",
                            IsDeleted = false,
                            Lectures = new List<Lecture>()
                            {
                                new Lecture{IsDeleted = false,Title= "General Understanding", BriefDescription = "Deals with the general knowledge of the student in several sections", FilePath = "https://media.proprofs.com/images/QM/user_images/2503852/New%20Project%20(63)(189).jpg"},
                                new Lecture{IsDeleted = false,Title= "World war II", BriefDescription = "Knowledge on the events that occured in world war ii", FilePath = "https://cdn.britannica.com/53/71453-131-6717E1F1/Pershing-troops-Mexico-World-War-I-1917.jpg"},
                                 new Lecture{IsDeleted = false,Title= "Geography", BriefDescription = "Understanding the geography of the earth", FilePath = "https://www.kids-world-travel-guide.com/images/geography_ball_300.jpg"},
                            },
                            Tutor = tutor1,
                        };

                        var exams = new List<Exam>()
                        {
                            new Exam{
                                    Course = course1, ExamTitle = "Test 1", DurationInMinute = 10, StartTime = DateTime.Now, DeadlineEndTime = DateTime.Now.AddYears(2),DeadlineStartTime = DateTime.Now,CoursePerentage = 10,
                                MultiChoiceQuestions = new List<McqQuestion>()
                                {
                                    new McqQuestion {
                                        Question = "What is the longest that an elephant has ever lived? (That we know of)",
                                        AnswerId = 2,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "17 years"},
                                            new McqOption {AnsId = 1, Option = "49 years"},
                                            new McqOption {AnsId = 2, Option = "86 years"},
                                            new McqOption {AnsId = 3, Option = "142 years"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "How many rings are on the Olympic flag?",
                                        AnswerId = 2,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "None"},
                                            new McqOption {AnsId = 1, Option = "4"},
                                            new McqOption {AnsId = 2, Option = "5"},
                                            new McqOption {AnsId = 3, Option = "7"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "How many rings are on the Olympic flag?",
                                        AnswerId = 2,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "None"},
                                            new McqOption {AnsId = 1, Option = "4"},
                                            new McqOption {AnsId = 2, Option = "5"},
                                            new McqOption {AnsId = 3, Option = "7"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "What is a tarsier?",
                                        AnswerId = 0,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "A primate"},
                                            new McqOption {AnsId = 1, Option = "A bird"},
                                            new McqOption {AnsId = 2, Option = "A lizard"},
                                            new McqOption {AnsId = 3, Option = "A cat"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "How did Spider-Man get his powers?",
                                        AnswerId = 3,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Military experiment gone awry"},
                                            new McqOption {AnsId = 1, Option = "Born with them"},
                                            new McqOption {AnsId = 2, Option = "Woke up with them after a strange dream"},
                                            new McqOption {AnsId = 3, Option = "Bitten by a radio active spider"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "In darts, what's the most points you can score with a single throw?",
                                        AnswerId = 2,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "20"},
                                            new McqOption {AnsId = 1, Option = "50"},
                                            new McqOption {AnsId = 2, Option = "60"},
                                            new McqOption {AnsId = 3, Option = "100"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "Which of these animals does NOT appear in the Chinese zodiac?",
                                        AnswerId = 0,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Bear"},
                                            new McqOption {AnsId = 1, Option = "Rabbit"},
                                            new McqOption {AnsId = 2, Option = "Dragon"},
                                            new McqOption {AnsId = 3, Option = "Dog"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "Who are known as Brahmins?",
                                        AnswerId = 1,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Surfers in California"},
                                            new McqOption {AnsId = 1, Option = "Members of india's highest caste"},
                                            new McqOption {AnsId = 2, Option = "It's a totally made up word"},
                                            new McqOption {AnsId = 3, Option = "Whites from africa"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "How many holes are on a standard bowling ball?",
                                        AnswerId = 1,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "2"},
                                            new McqOption {AnsId = 1, Option = "3"},
                                            new McqOption {AnsId = 2, Option = "5"},
                                            new McqOption {AnsId = 3, Option = "10"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "Would a Catholic living in the United States ever celebrate Easter in May?",
                                        AnswerId = 1,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Yes"},
                                            new McqOption {AnsId = 1, Option = "No"},
                                            new McqOption {AnsId = 2, Option = "Sometimes"},
                                            new McqOption {AnsId = 3, Option = "All of the above"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "What are the main colors on the flag of Spain?",
                                        AnswerId = 3,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Black and Yellow"},
                                            new McqOption {AnsId = 1, Option = "Green and white"},
                                            new McqOption {AnsId = 2, Option = "Blue and white"},
                                            new McqOption {AnsId = 3, Option = "Red and Yellow"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "What is the name of this symbol: ¶",
                                        AnswerId = 1,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Fermata"},
                                            new McqOption {AnsId = 1, Option = "Pilcrow"},
                                            new McqOption {AnsId = 2, Option = "Interrobang"},
                                            new McqOption {AnsId = 3, Option = "Biltong"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "In the nursery rhyme, how many blackbirds were baked in a pie?",
                                        AnswerId = 2,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "4"},
                                            new McqOption {AnsId = 1, Option = "11"},
                                            new McqOption {AnsId = 2, Option = "24"},
                                            new McqOption {AnsId = 3, Option = "99"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "What is a pomelo?",
                                        AnswerId = 2,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "An old-fashioned punching bag"},
                                            new McqOption {AnsId = 1, Option = "A breed of dog"},
                                            new McqOption {AnsId = 2, Option = "The largest citrus tree"},
                                            new McqOption {AnsId = 3, Option = "A university"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "Who killed Greedo?",
                                        AnswerId = 1,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Hannibal Lecter"},
                                            new McqOption {AnsId = 1, Option = "Han solo"},
                                            new McqOption {AnsId = 2, Option = "Hermione Granger"},
                                            new McqOption {AnsId = 3, Option = "Hercules"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "Are giant pandas a type of bear?",
                                        AnswerId = 0,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "Yes"},
                                            new McqOption {AnsId = 1, Option = "No"},
                                            new McqOption {AnsId = 2, Option = "All of the above"},
                                            new McqOption {AnsId = 3, Option = "Sometimes true"},
                                        },
                                        Score = 1,
                                    },

                                    new McqQuestion {
                                        Question = "How many points is the letter X worth in English-language Scrabble?",
                                        AnswerId = 0,
                                        Options = new List<McqOption>()
                                        {
                                            new McqOption {AnsId = 0, Option = "None"},
                                            new McqOption {AnsId = 1, Option = "8"},
                                            new McqOption {AnsId = 2, Option = "11"},
                                            new McqOption {AnsId = 3, Option = "20"},
                                        },
                                        Score = 1,
                                    },
                                },
                                TheoryQuestions = new List<TheoryQuestion>
                                {
                                    new TheoryQuestion {Question = "In what year did Nigeria become a repubulic?", Score = 2},
                                    new TheoryQuestion {Question = "Explain the events that occured in the abolishment of slave trade", Score=10}
                                },
                                TotalScore = 28,
                            },
                        };
                        context.Exam.AddRange(exams);


                        userName = "Kemiadeosun@gmail.com";
                        password = "kemi";
                        var tutorUser2 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(tutorUser2, password);
                        await _userManager.AddToRoleAsync(tutorUser2, AppConstant.LecturerRole);

                        var tutor2 = new Tutor { UserId = tutorUser2.Id, Email = tutorUser2.Email, Fullname = "Dr. Kemi Adeosun" };

                        var course2 = new Course
                        {
                            CourseCode = "MEG 403",
                            CourseTitle = "Thermodynamics",
                            DateCreated = DateTime.Now,
                            DateCreatedUtc = DateTime.Now,
                            DateModified = DateTime.Now,
                            DateModifiedUtc = DateTime.Now,
                            Description = "The study of energy, energy transformations and it relation to matter.",
                            IsDeleted = false,
                            Lectures = new List<Lecture>() {},
                            Tutor = tutor2,
                        };

                        userName = "keneprince@gmail.com";
                        password = "kene";
                        var tutorUser3 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(tutorUser3, password);
                        await _userManager.AddToRoleAsync(tutorUser3, AppConstant.LecturerRole);

                        var tutor3 = new Tutor { UserId = tutorUser3.Id, Email = tutorUser3.Email, Fullname = "Engr. Kenechukwu" };

                        var course3 = new Course
                        {
                            CourseCode = "EEG 325",
                            CourseTitle = "Electronics",
                            DateCreated = DateTime.Now,
                            DateCreatedUtc = DateTime.Now,
                            DateModified = DateTime.Now,
                            DateModifiedUtc = DateTime.Now,
                            Description = "Electronics in electrical engineering",
                            IsDeleted = false,
                            Lectures = new List<Lecture>() { },
                            Tutor = tutor3,
                        };

                        userName = "alarindeopeyemi@gmail.com";
                        password = "alarinde";
                        var tutorUser4 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(tutorUser4, password);
                        await _userManager.AddToRoleAsync(tutorUser4, AppConstant.LecturerRole);

                        var tutor4 = new Tutor { UserId = tutorUser4.Id, Email = tutorUser4.Email, Fullname = "Engr. Kenechukwu" };

                        var course4 = new Course
                        {
                            CourseCode = "CPE 311",
                            CourseTitle = "Introduction to programming",
                            DateCreated = DateTime.Now,
                            DateCreatedUtc = DateTime.Now,
                            DateModified = DateTime.Now,
                            DateModifiedUtc = DateTime.Now,
                            Description = "Students are introduced to the basics of programming using the C++ language",
                            IsDeleted = false,
                            Lectures = new List<Lecture>() { },
                            Tutor = tutor4,
                        };

                        context.Course.Add(course1);
                        context.Course.Add(course2);
                        context.Course.Add(course3);
                        context.Course.Add(course4);
                        await context.SaveChangesAsync();

                    }


                    if (await _roleManager.FindByNameAsync(AppConstant.StudentRole) == null)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(AppConstant.StudentRole));
                        var courses = context.Course;

                        string userName = "josepholadunjoye@gmail.com";
                        string password = "joseph";
                        var studentUser1 = new ApplicationUser { UserName = userName, Email = userName };
                        var result = await _userManager.CreateAsync(studentUser1, password);
                        await _userManager.AddToRoleAsync(studentUser1, AppConstant.StudentRole);
                        var student1 = new Student { UserId = studentUser1.Id, Email = studentUser1.Email, FirstName = "Joseph",
                        LastName = "Oladunjoye",MatricNumber = "150302010"};

                        userName = "benitaohane@gmail.com";
                        password = "benita";
                        var studentUser2 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(studentUser2, password);
                        await _userManager.AddToRoleAsync(studentUser2, AppConstant.StudentRole);
                        var student2 = new Student
                        {
                            UserId = studentUser2.Id,
                            Email = studentUser2.Email,
                            FirstName = "Benita",
                            LastName = "Ohanekwurum",
                            MatricNumber = "150302011",
                            Courses = courses.ToList(),
                        };

                        userName = "timilehinKayode@gmail.com";
                        password = "timilehin";
                        var studentUser3 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(studentUser3, password);
                        await _userManager.AddToRoleAsync(studentUser3, AppConstant.StudentRole);
                        var student3 = new Student
                        {
                            UserId = studentUser3.Id,
                            Email = studentUser3.Email,
                            FirstName = "Timilehin",
                            LastName = "Kayode",
                            MatricNumber = "150302041",
                            Courses = courses.ToList(),
                        };

                        userName = "TifaseAyomide@gmail.com";
                        password = "tifase";
                        var studentUser4 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(studentUser4, password);
                        await _userManager.AddToRoleAsync(studentUser4, AppConstant.StudentRole);
                        var student4 = new Student
                        {
                            UserId = studentUser3.Id,
                            Email = studentUser3.Email,
                            FirstName = "Tifase",
                            LastName = "Ayomide",
                            MatricNumber = "150302036",
                            Courses = courses.ToList(),
                        };

                        userName = "TomisinAgboola@gmail.com";
                        password = "tomisin";
                        var studentUser5 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(studentUser5, password);
                        await _userManager.AddToRoleAsync(studentUser5, AppConstant.StudentRole);
                        var student5 = new Student
                        {
                            UserId = studentUser5.Id,
                            Email = studentUser5.Email,
                            FirstName = "Tomisin",
                            LastName = "Agboola",
                            MatricNumber = "150302033",
                            Courses = courses.ToList(),
                        };

                        userName = "ToluhiAnu@gmail.com";
                        password = "toluhi";
                        var studentUser6 = new ApplicationUser { UserName = userName, Email = userName };
                        result = await _userManager.CreateAsync(studentUser6, password);
                        await _userManager.AddToRoleAsync(studentUser6, AppConstant.StudentRole);
                        var student6 = new Student
                        {
                            UserId = studentUser6.Id,
                            Email = studentUser6.Email,
                            FirstName = "Toluhi",
                            LastName = "Anuoluwapo",
                            MatricNumber = "150302035",
                            Courses = courses.ToList(),
                        };

                        context.Student.AddRange(student1, student2, student3,student4, student5, student6);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
