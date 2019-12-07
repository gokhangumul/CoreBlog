using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using CoreBlog.Entity.DbModel;
using CoreBlog.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreBlog.Web.Controllers.Back
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly IImageService imgservice;
        private readonly IHostingEnvironment _environment;
        public PostController(IPostService postService, ICategoryService categoryService, IImageService imgservice, IHostingEnvironment _environment)
        {
            this.postService = postService ?? throw new ArgumentNullException(nameof(postService));
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(postService));
            this.imgservice = imgservice ?? throw new ArgumentException(nameof(imgservice));
            this._environment = _environment ?? throw new ArgumentException(nameof(_environment));
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllPost()
        {
            TempData["pageTitle"] = "Post | Get All Post";
            TempData["pageInfo1"] = "Home";
            TempData["pageInfo2"] = "Index";
            try
            {

                var result = await postService.GetWithCategory();
                return View(result);
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Error");
            }

        }

        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            try
            {
                ModelState.Clear();
                TempData["pageTitle"] = "Post | CreatePost";
                TempData["pageInfo1"] = "Post";
                TempData["pageInfo2"] = "GetAllPost";
                var result = await categoryService.GetAll(x => x.IsActive == true);
                ViewBag.categoryItem = new SelectList(result, "Id", "Title");
                Post post = new Post();
                return View(post);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post, IFormFile images)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    if (images != null)
                    {
                        bool imgcheck = ImageCheck(images);
                        if (imgcheck == false)
                        {
                            TempData["pageTitle"] = "Post | CreatePost";
                            TempData["pageInfo1"] = "Post";
                            TempData["pageInfo2"] = "GetAllPost";
                            var result = await categoryService.GetAll(x => x.IsActive == true);
                            ViewBag.categoryItem = new SelectList(result, "Id", "Title");
                            TempData["imgerror"] = "Lütfen sadece jpg,gif,png,jpeg uzantılı dosya seçiniz.";
                            return View(post);
                        }
                        else
                        {
                            UrlHelper uri = new UrlHelper();
                            var url = uri.Url(post.Title);
                            var uniqkey = Guid.NewGuid().ToString();
                            post.CreatedDate = DateTime.Now;
                            post.Url = url;
                            post.UniqKey = uniqkey;
                            post.AuthorName = User.Identity.Name;
                            post.Hit = 0;
                            string gui = Guid.NewGuid().ToString();
                            post.PostDetailHeaderImage = gui + images.FileName;
                            var result = await postService.Create(post);
                            string othergui = Guid.NewGuid().ToString();
                            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\postuploads", gui + images.FileName);
                            if (images.Length > 0)
                            {
                                using (var filestream = new FileStream(uploads, FileMode.Create))
                                {
                                    await images.CopyToAsync(filestream);
                                }
                            }

                            return RedirectToAction("GetAllPost");
                        }

                    }
                    else
                    {
                        UrlHelper uri = new UrlHelper();
                        var url = uri.Url(post.Title);
                        var uniqkey = Guid.NewGuid().ToString();
                        post.CreatedDate = DateTime.Now;
                        post.Url = url;
                        post.UniqKey = uniqkey;
                        post.AuthorName = User.Identity.Name;
                        post.Hit = 0;
                        var result = await postService.Create(post);
                        return RedirectToAction("GetAllPost");
                    }

                }
                else
                {
                    if (post.CategoryId == 0)
                    {
                        ModelState["CategoryId"].Errors.Clear();
                        ModelState.AddModelError("CategoryId", "Post kategorisi boş geçilemez");
                    }
                    TempData["pageTitle"] = "Post | CreatePost";
                    TempData["pageInfo1"] = "Post";
                    TempData["pageInfo2"] = "GetAllPost";
                    var result = await categoryService.GetAll(x => x.IsActive == true);
                    ViewBag.categoryItem = new SelectList(result, "Id", "Title");
                    return View(post);

                }

            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        private bool ImageCheck(IFormFile images)
        {
            bool check = true;
            if (images.ContentType != "image/jpg" && images.ContentType != "image/jpeg" && images.ContentType != "image/png" && images.ContentType != "image/JPG")
            {
                check = false;
            }
            return check;
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePost(string uniqkey)
        {
            try
            {
                TempData["pageTitle"] = "Post | UpdatePost";
                TempData["pageInfo1"] = "Post";
                TempData["pageInfo2"] = "GetAllPost";
                var result = await postService.Get(x => x.UniqKey == uniqkey);
                var catresult = await categoryService.GetAll();
                ViewBag.categoryItem = new SelectList(catresult, "Id", "Title", result.CategoryId);
                return View(result);
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Error");

            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost(Post post, IFormFile images)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (images != null)
                    {
                        if (post.PostDetailHeaderImage != null)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\postuploads", post.PostDetailHeaderImage);
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                        }
                        string gui = Guid.NewGuid().ToString();
                        var upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Back\\postuploads", gui + images.FileName);
                        if (images.Length > 0)
                        {
                            using (var filestream = new FileStream(upload, FileMode.Create))
                            {
                                await images.CopyToAsync(filestream);
                            }
                        }
                        post.UpdatedDate = DateTime.Now;
                        post.PostDetailHeaderImage = gui + images.FileName;
                        await postService.Update(post);
                        return RedirectToAction("GetAllPost");
                    }
                    else
                    {
                        post.UpdatedDate = DateTime.Now;
                        await postService.Update(post);
                        return RedirectToAction("GetAllPost");

                    }

                }
                else
                {

                    return View();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Error");
            }


        }

        /*
        public async Task<IActionResult> PassiveorActivePost(string uniqkey)
        {
            try
            {
                await postService.PassOrActive(uniqkey);
                return RedirectToAction("GetAllPost");
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        */

        /*
        public async Task<IActionResult> GetImages(string uniqkey)
        {

            try
            {
                TempData["pageTitle"] = "Post | GetImages";
                TempData["pageInfo1"] = "Post";
                TempData["pageInfo2"] = "GetAllPost";
                var result = await postService.GetWithImage(uniqkey);
                return View(result);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        */
    }
}