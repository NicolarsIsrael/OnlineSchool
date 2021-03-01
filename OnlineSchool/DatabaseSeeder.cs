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
                        await _roleManager.CreateAsync(new ApplicationRole(AppConstant.LecturerRole));

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
