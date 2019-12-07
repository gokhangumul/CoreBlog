using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlog.Web.Controllers.Back
{
   [Authorize]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Home | Index";
            TempData["pageInfo1"] = "Home";
            TempData["pageInfo2"] = "Index";
            return View();
        }
    }
}