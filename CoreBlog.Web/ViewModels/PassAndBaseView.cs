using CoreBlog.Data.Concrete.IdendityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.ViewModels
{
    public class PassAndBaseView
    {
        public AppUser AppUser { get; set; }
        public PassUpdateViewModel PassUpdateViewModel { get; set; }
    }
}
