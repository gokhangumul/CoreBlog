using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlog.Web.Controllers.Front
{
    public class IletisimController : Controller
    {
        private readonly ISettingsService settingsService;

        public IletisimController(ISettingsService settingsService)
        {
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }

        public async Task< IActionResult> Index()
        {
            var result = await settingsService.Get(x => x.SettingName == "General Settings");
            TempData["blogName"] = result.BlogName;
            TempData["mainHead"] = result.MainHeader;
            TempData["subHead"] = result.MainSubHeader;
            TempData["title"] = "İletişim";
            TempData["path"] = "pageimg";
            TempData["headerImage"] = result.MainHeaderImage;
            TempData["css"] = "col-lg-8 col-md-10 mx-auto";
            return View();
            
        }
    }
}