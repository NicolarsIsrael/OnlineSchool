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
                        await _roleManager.CreateAsync(new ApplicationRole(AppConstant.StudentRole));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
