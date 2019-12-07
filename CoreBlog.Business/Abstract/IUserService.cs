using CoreBlog.Data.Concrete.IdendityCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Business.Abstract
{
    public interface IUserService
    {
        Task<bool> LoginUserAsync(string email, string password);
        Task LogOut();
        Task<AppUser> GetUser(string username);
        Task UpdateUser(AppUser user);
        Task UpdatePass(string username, string pass);
        Task<bool> HashPasss(string username,string pass);
    }
}
