using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using CoreBlog.Entity.DbModel;
using CoreBlog.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlog.Web.Controllers.Back
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsService settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }

        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> AllSettings()
        {
            try
            {
                TempData["pageTitle"] = "Settings | Get All Settings";
                TempData["pageInfo1"] = "Home";
                TempData["pageInfo2"] = "Index";
                var result = await settingsService.GetAll();
                return View(result);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateSettings(string uniqkey)
        {
            try
            {
                TempData["pageTitle"] = "Settings | Update Settings";
                TempData["pageInfo1"] = "Settings";
                TempData["pageInfo2"] = "AllSettings";
                var result = await settingsService.Get(x => x.UniqKey == uniqkey);
                return View(result);
            }
            catch (Exception)
            {
                
                return RedirectToAction("ErrorPage", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSettings(Setting setting, IFormFile images)
        {
            try
            {
                if (images != null)
                {
                    using (ImageCheck check = new ImageCheck())
                    {
                        bool imgcheck = check.CheckImageUzan(images);
                        if (imgcheck != false)
                        {
                            if (setting.MainHeaderImage != null)
                            {
                                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\pageimg", setting.MainHeaderImage);
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                    string gui = Guid.NewGuid().ToString();
                                    var newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\pageimg", gui + images.FileName);
                                    if (images.Length > 0)
                                    {
                                        using (var filestream = new FileStream(newpath, FileMode.Create))
                                        {
                                            await images.CopyToAsync(filestream);
                                        }
                                    }
                                    setting.MainHeaderImage = gui + images.FileName;
                                    await settingsService.Update(setting);
                                    return RedirectToAction("AllSettings", "Settings");

                                }
                                else
                                {
                                    return RedirectToAction("ErrorPage", "Error");
                                }

                            }
                            else
                            {
                                string gui = Guid.NewGuid().ToString();
                                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\pageimg", gui + images.FileName);
                                if (images.Length > 0)
                                {
                                    using (var filestream = new FileStream(path, FileMode.Create))
                                    {
                                        await images.CopyToAsync(filestream);
                                    }
                                }
                                setting.MainHeaderImage = gui + images.FileName;
                                await settingsService.Update(setting);
                                return RedirectToAction("AllSettings", "Settings");
                            }
                        }
                        else
                        {
                            TempData["pageTitle"] = "Settings | Update Settings";
                            TempData["pageInfo1"] = "Settings";
                            TempData["pageInfo2"] = "AllSettings";
                            TempData["imgerror"] = "Lütfen sadece jpg,gif,png,jpeg uzantılı dosya seçiniz.";
                            return View(setting);
                        }
                    }
                }
                else
                {
                    await settingsService.Update(setting);
                    return RedirectToAction("AllSettings", "Settings");
                }
            }
            catch (Exception e)
            {

                return RedirectToAction("ErrorPage", "Error",e.Message);
            }
        }
    }
}