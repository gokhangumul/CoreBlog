using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using CoreBlog.Entity.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlog.Web.Controllers.Back
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                TempData["pageTitle"] = "Category | GetAllCategory";
                TempData["pageInfo1"] = "Home";
                TempData["pageInfo2"] = "Index";
                var result = await categoryService.GetAll();
                return View(result);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            try
            {
                ModelState.Clear();
                TempData["pageTitle"] = "Category | Create Category";
                TempData["pageInfo1"] = "Category";
                TempData["pageInfo2"] = "GetAllCategory";
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.CreatedTime = DateTime.Now;
                    string gui = Guid.NewGuid().ToString();
                    category.UniqKey = gui;
                    await categoryService.Create(category);
                    return RedirectToAction("GetAllCategory", "Category");
                }
                else
                {
                    TempData["pageTitle"] = "Category | Create Category";
                    TempData["pageInfo1"] = "Category";
                    TempData["pageInfo2"] = "GetAllCategory";
                    return View(category);
                }
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string uniqkey)
        {
            try
            {
                TempData["pageTitle"] = "Category | Update Category";
                TempData["pageInfo1"] = "Category";
                TempData["pageInfo2"] = "GetAllCategory";
                var result = await categoryService.Get(x => x.UniqKey == uniqkey);
                return View(result);
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            try
            {
                category.UpdatedTime = DateTime.Now;
                await categoryService.Update(category);
                return RedirectToAction("GetAllCategory", "Category");
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage", "Error");
            }
        }
    }
}