using CoreBlog.Business.Abstract;
using CoreBlog.Data.Concrete.IdendityCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Business.Concrete
{
    public class UserServiceManager : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;

        public UserServiceManager(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<AppUser> GetUser(string username)
        {
            var result = await userManager.FindByNameAsync(username);
            return result;

        }

        public async Task<bool> HashPasss(string username, string pass)
        {
            try
            {
                var result = await userManager.FindByNameAsync(username);
                string hashpass = userManager.PasswordHasher.HashPassword(result, pass);
                if (hashpass != result.PasswordHash)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<bool> LoginUserAsync(string email, string password)
        {
            try
            {
                await signInManager.SignOutAsync();
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
               
                    return false;
                }
                var result = await signInManager.PasswordSignInAsync(user, password,false,false);
                if (result.Succeeded)
                {
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            throw new NotImplementedException();
        }

        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task UpdatePass(string username, string pass)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User Notfound");
            }
            else
            {
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user,pass);
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Error Pass Change");
                }
            }
        }

        public async Task UpdateUser(AppUser user)
        {
            try
            {
                var result = await userManager.FindByNameAsync(user.UserName);
                if (result != null)
                {
                    result.ProfileImage = user.ProfileImage;
                    result.Email = user.Email;
                    result.Name = user.Name;
                    result.SurName = user.SurName;
                    result.JobDesc = user.JobDesc;
                    var check=  await userManager.UpdateAsync(result);
                    if (check.Succeeded ==false)
                    {
                        throw new Exception("Hata");
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
