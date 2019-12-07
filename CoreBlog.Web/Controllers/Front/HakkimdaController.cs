using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlog.Web.Controllers.Front
{
    public class HakkimdaController : Controller
    {
        private readonly ISettingsService settingsService;

        public HakkimdaController(ISettingsService settingsService)
        {
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result =await settingsService.Get(x => x.SettingName == "General Settings");
                TempData["blogName"] = result.BlogName;
                TempData["mainHead"] = result.MainHeader;
                TempData["subHead"] = result.MainSubHeader;
                TempData["title"] = "Hakkımda";
                TempData["path"] = "pageimg";
                TempData["headerImage"] = result.MainHeaderImage;
                TempData["about"] = result.About;
                TempData["css"] = "col-lg-8 col-md-10 mx-auto";
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
            
        }
    }
}