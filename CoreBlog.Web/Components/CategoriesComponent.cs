using CoreBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.Components
{
    public class CategoriesComponent:ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoriesComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }
        public IViewComponentResult Invoke()
        {
            var result = categoryService.GetAllWithPost(x => x.IsActive == true).Result;
            return View(result);
        }
    }
}
