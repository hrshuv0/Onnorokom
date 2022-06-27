using Microsoft.AspNetCore.Identity;

namespace Assignment.Data.Static;

public class SeedData
{
    public static async Task Seed(IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();
        
        // roles
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        // admin
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var admin = await userManager.FindByIdAsync("admin");
        if (admin is null)
        {
            var adminUser = new IdentityUser()
            {
                UserName = "admin"
            };
            await userManager.CreateAsync(adminUser, "1234");
            await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
        }
            
        // user

        var user = await userManager.FindByIdAsync("user");
        if (user is null)
        {
            var memberUser = new IdentityUser()
            {
                UserName = "user"
            };
            await userManager.CreateAsync(memberUser, "1234");
            await userManager.AddToRoleAsync(memberUser, UserRoles.User);
        }
    }
}