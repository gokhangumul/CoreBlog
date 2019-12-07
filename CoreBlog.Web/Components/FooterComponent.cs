using CoreBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.Components
{
    public class FooterComponent:ViewComponent
    {
        private readonly ISettingsService settingsService;

        public FooterComponent(ISettingsService settingsService)
        {
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }
        public IViewComponentResult Invoke()
        {
            var result = settingsService.Get(x => x.SettingName == "General Settings").Result;
            return View(result);
        }
    }
}
