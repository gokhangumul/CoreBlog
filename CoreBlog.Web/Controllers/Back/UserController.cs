using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using CoreBlog.Data.Concrete.IdendityCore;
using CoreBlog.Web.Helpers;
using CoreBlog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlog.Web.Controllers.Back
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginmodel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUserAsync(loginmodel.Email, loginmodel.Password);
                if (result == true)
                {
                    var user = User.Identity.Name;
                    return Redirect(returnUrl ?? "/Home/Index");
                }
                else
                {
                    ViewBag.error = true;
                    ModelState.AddModelError("Hata", "Mail adresi veya parola hatalı.");
                    return View();
                }
            }
            else
            {
                return View(loginmodel);
            }
            
        }
        public async Task LogOut()
        {
           await userService.LogOut();
           
        }
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                TempData["pageTitle"] = "User | Get Profile";
                TempData["pageInfo1"] = "Home";
                TempData["pageInfo2"] = "Index";
                var result = await userService.GetUser(User.Identity.Name);
                var passmodel = new PassUpdateViewModel();
                PassAndBaseView model = new PassAndBaseView
                {
                    AppUser = result,
                    PassUpdateViewModel =passmodel
                };
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("ErrorPage", "Error");
            }
            
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateBase(AppUser appUser, IFormFile images)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(appUser.Email))
                    {

                        TempData["pageTitle"] = "User | Get Profile";
                        TempData["pageInfo1"] = "Home";
                        TempData["pageInfo2"] = "Index";
                        if (String.IsNullOrEmpty(appUser.Email))
                        {
                            TempData["emailError"] = "Email boş geçilemez.";
                        }
                        var result = await userService.GetUser(User.Identity.Name);
                        PassAndBaseView model = new PassAndBaseView
                        {
                            AppUser = result
                        };
                        return View("GetProfile", model);
                    }
                    else
                    {
                        if (images != null)
                        {
                            using (var img = new ImageCheck())
                            {
                                bool check = img.CheckImageUzan(images);
                                if (check != false)
                                {
                                    if (appUser.ProfileImage != null)
                                    {
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\userimg", appUser.ProfileImage);
                                        if (System.IO.File.Exists(path))
                                        {
                                            System.IO.File.Delete(path);
                                        }
                                    }
                                    string gui = Guid.NewGuid().ToString();
                                    var upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\userimg", gui + images.FileName);
                                    if (images.Length > 0)
                                    {
                                        using (var stream = new FileStream(upload, FileMode.Create))
                                        {
                                            await images.CopyToAsync(stream);
                                        }
                                    }
                                    appUser.ProfileImage = gui + images.FileName;
                                    await userService.UpdateUser(appUser);
                                    return RedirectToAction("GetProfile", "User");
                                }
                                else
                                {
                                    TempData["pageTitle"] = "User | Get Profile";
                                    TempData["pageInfo1"] = "Home";
                                    TempData["pageInfo2"] = "Index";
                                    TempData["imgerror"] = "Belirtilen formatlara uygun dosya yükleyiniz (jpg,JPG,png,jpeg)";
                                    var result = await userService.GetUser(User.Identity.Name);
                                    PassAndBaseView model = new PassAndBaseView
                                    {
                                        AppUser = result
                                    };
                                    return View("GetProfile", model);
                                }
                            }
                           
                        }
                        else
                        {
                            await userService.UpdateUser(appUser);
                            return RedirectToAction("GetProfile", "User");
                        }
                    }
                   
                   
                }
                else
                {
                    TempData["pageTitle"] = "User | Get Profile";
                    TempData["pageInfo1"] = "Home";
                    TempData["pageInfo2"] = "Index";
                    if (String.IsNullOrEmpty(appUser.Email))
                    {
                        TempData["emailError"] = "Email boş geçilemez.";
                    }
                    var result = await userService.GetUser(User.Identity.Name);
                    PassAndBaseView model = new PassAndBaseView
                    {
                        AppUser = result
                    };
                    return View("GetProfile",model);
                }
                
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(PassAndBaseView passAndBaseView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (passAndBaseView.PassUpdateViewModel.NewPass != passAndBaseView.PassUpdateViewModel.AgainNewPass)
                    {
                        TempData["passMatch"] = "Girilen parolalar birbiriyle eşleşmiyor.";
                        return RedirectToAction("GetProfile", "User");
                    }
                    else
                    {
                        await userService.UpdatePass(User.Identity.Name, passAndBaseView.PassUpdateViewModel.NewPass);
                        return RedirectToAction("GetProfile", "User");
                    }
                }
                else
                {

                    TempData["pageTitle"] = "User | Get Profile";
                    TempData["pageInfo1"] = "Home";
                    TempData["pageInfo2"] = "Index";
                    var result = await userService.GetUser(User.Identity.Name);
                    PassAndBaseView model = new PassAndBaseView
                    {
                        AppUser = result,
                        PassUpdateViewModel = new PassUpdateViewModel()
                    };
                    return View("GetProfile", model);
                }
            
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }

        }
       
    }
}