using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Mail adresini boş geçmeyiniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Şifre alanını boş geçmeyiniz.")]
        public string Password { get; set; }

    }
}
