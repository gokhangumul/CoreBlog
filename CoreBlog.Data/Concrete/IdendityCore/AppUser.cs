using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreBlog.Data.Concrete.IdendityCore
{
    public class AppUser:IdentityUser
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Kullanıcı soyadı boş geçilemez.")]
        public string SurName { get; set; }
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Meslek boş geçilemez.")]
        public string JobDesc { get; set; }
    }
}
