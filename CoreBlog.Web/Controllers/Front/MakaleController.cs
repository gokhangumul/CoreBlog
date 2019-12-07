using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using CoreBlog.Entity.DbModel;
using CoreBlog.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlog.Web.Controllers.Front
{
    public class MakaleController : Controller
    {
        private readonly IPostService postService;
        private readonly ISettingsService settingsService;
        public MakaleController(IPostService postService, ISettingsService settingsService)
        {
            this.postService = postService ?? throw new ArgumentNullException(nameof(postService));
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }
        private async Task<Setting> GetPageInfo()
        {
            return await settingsService.Get(x => x.SettingName == "General Settings");
        }
        public async Task<IActionResult> Index(string category,int page=1)
        {
           
            try
            {
                var result = await postService.GetAll(x => x.IsActive == true);
                if (category != null)
                {
                   result = await postService.GetAll(x => (x.Category.Title==category) && (x.IsActive==true));
                }
                
                
                    TempData["mainHead"] = GetPageInfo().Result.MainHeader;
                    TempData["subHead"] = GetPageInfo().Result.MainSubHeader;
                    TempData["blogName"] = GetPageInfo().Result.BlogName;
                    TempData["path"] = "pageimg";
                    TempData["headerImage"] = GetPageInfo().Result.MainHeaderImage;
                    TempData["title"] = "Ana Sayfa";
                    TempData["css"] = "col-lg-12 col-md-10 mx-auto";


                    var count = result.Count();
                    int pagesize = 5;
                    result = result.Skip((page - 1) * pagesize).Take(pagesize);
                    var response = new PostList()
                    {
                        Posts = result.ToList(),
                        PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemPerPage = pagesize,
                            Total = count,
                            CategoryName=category
                        }
                    };

                    if (response.Posts.Count > 0)
                    {
                        return View(response);
                    }
                    else
                    {

                        return RedirectToAction("ErrorPage", "Error");
                    }
                
              
               
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
            
        }
        public async Task<IActionResult> Detay(string url)
        {
            try
            {
                
                var result = await postService.GetPost(url);
                TempData["title"] = "Detay";
                TempData["mainHead"] = result.Title;
                TempData["subHead"] = result.Description;
                TempData["postAuthor"] = result.AuthorName;
                TempData["postCreated"] = result.CreatedDate.ToString("dd MMMM yyyy");
                TempData["blogName"] = GetPageInfo().Result.BlogName;
                TempData["css"] = "col-lg-8 col-md-10 mx-auto";
                if (result.PostDetailHeaderImage != null)
                {
                    TempData["path"] = "postuploads";
                    TempData["headerImage"] = result.PostDetailHeaderImage;
                }
                else
                {
                    TempData["path"] = "pageimg";
                    TempData["headerImage"] = GetPageInfo().Result.MainHeaderImage;
                }
                return View(result);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        
    }
}