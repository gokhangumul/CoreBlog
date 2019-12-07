using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.ViewModels
{
    public class PassUpdateViewModel
    {
       
        [Required(ErrorMessage ="Yeni parola boş geçilemez.")]
        public string NewPass { get; set; }

        [Required(ErrorMessage ="Yeni parola tekrarı boş geçilemez.")]
        public string AgainNewPass { get; set; }
    }
}
