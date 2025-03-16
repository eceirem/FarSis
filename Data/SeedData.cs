using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using FarSis.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FarSis.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<SeedData> logger)
        {
            var context = serviceProvider.GetRequiredService<FarSisContext>();

            try
            {
                // Add departments if not already in the database
                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department { Name = "İdari İşler", Id = 1 },
                        new Department { Name = "Teknik Destek", Id = 2 },
                        new Department { Name = "Kullanıcı Destek", Id = 3 },
                        new Department { Name = "Mali İşler", Id = 4 },
                        new Department { Name = "Bilgi Güvenliği", Id = 5 },
                        new Department { Name = "Web Yazılım", Id = 6 },
                        new Department { Name = "Yazılım", Id = 7 },
                        new Department { Name = "Sistem", Id = 8 },
                        new Department { Name = "Ağ", Id = 9 },
                        new Department { Name = "Başkan", Id = 10 },
                        new Department { Name = "Editör", Id = 11 }
                    );
                    await context.SaveChangesAsync();
                    logger.LogInformation("Departments added successfully.");
                }

                // Create Roles if they don't exist
                string[] roleNames = { "Principle", "Editor", "Leader" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                        logger.LogInformation($"Role {roleName} created.");
                    }
                }

                // Create the Principle and Editor users (only 1 principle and 1 editor)
                var principleUser = new User
                {
                    UserName = "Barış",
                    Email = "principle@farsis.com",
                    DepartmentId = 10
                };

                var result = await userManager.CreateAsync(principleUser, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(principleUser, "Principle");
                    logger.LogInformation("Principle user created.");
                }

                var editorUser = new User
                {
                    UserName = "Okan",
                    Email = "editor@farsis.com",
                    DepartmentId = 11
                };

                result = await userManager.CreateAsync(editorUser, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(editorUser, "Editor");
                    logger.LogInformation("Editor user created.");
                }

                // Create the Leaders for each department
                var departments = context.Departments.ToList();
                var leaderNames = new[]
                {
            "Ece İrem", "Ali Veli", "Ayşe Yılmaz", "Mehmet Demir", "Fatma Çelik",
            "Ahmet Korkmaz", "Zeynep Aksoy", "Okan Özdemir", "Hüseyin Karaca"
        };

                for (int i = 0; i < departments.Count() - 2; i++)
                {
                    var user = new User
                    {
                        UserName = leaderNames[i],
                        Email = $"{leaderNames[i].ToLower()}@farsis.com",
                        DepartmentId = departments[i].Id
                    };

                    result = await userManager.CreateAsync(user, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Leader");
                        logger.LogInformation($"Leader user {leaderNames[i]} created.");
                    }
                }

                await context.SaveChangesAsync();
                logger.LogInformation("Database seeding completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        

        await context.SaveChangesAsync();
        }
    }
}
