using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Data.Concrete.IdendityCore
{
    public class SeedIdentity
    {
        public static async Task CreateUser(IServiceProvider serviceProvider,IConfiguration configuration)
        {
            var usermanager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var username = configuration["Data:AdminUser:username"];
            var email = configuration["Data:AdminUser:email"];
            var password = configuration["Data:AdminUser:password"];
            var role = configuration["Data:AdminUser:role"];
            if(await usermanager.FindByNameAsync(username) == null)
            {
                if(await usermanager.FindByNameAsync(role) == null)
                {
                    await rolemanager.CreateAsync(new IdentityRole(role));
                }
                AppUser user = new AppUser
                {
                    UserName = username,
                    Email = email,
                    Name = "İbrahim",
                    SurName = "Gümül"
                };
                IdentityResult result = await usermanager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(user, role);
                }
            }
           
            
        }
    }
}
