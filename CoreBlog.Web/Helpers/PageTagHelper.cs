using CoreBlog.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;


namespace CoreBlog.Web.Helpers
{
    [HtmlTargetElement("div",Attributes ="page-model")]
    public class PageTagHelper:TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public PageTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory ?? throw new ArgumentNullException(nameof(urlHelperFactory));
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            var result = new TagBuilder("div");
            var tp = PageModel.TotalPages();
            if (PageModel.CurrentPage > 1)
            {
                var tagilk = new TagBuilder("a");
                if (PageModel.CategoryName != null)
                {
                    tagilk.Attributes["href"] = urlHelper.Action(PageAction, new { page = PageModel.CurrentPage - 1 ,category=PageModel.CategoryName});
                }
                else
                {
                    tagilk.Attributes["href"] = urlHelper.Action(PageAction, new { page = PageModel.CurrentPage - 1 });
                }
     
                tagilk.InnerHtml.Append("<<");
                tagilk.AddCssClass("btn btn-primary btn-circle ml-1");
                result.InnerHtml.AppendHtml(tagilk);
            }
            for (int i = 1; i <= tp; i++)
            {
                var tag = new TagBuilder("a");
                if (PageModel.CategoryName != null)
                {
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i , category = PageModel.CategoryName });
                }
                else
                {
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                }
               
                tag.InnerHtml.Append(i.ToString());
                tag.AddCssClass("btn btn-primary btn-circle ml-1");
                result.InnerHtml.AppendHtml(tag);
              
            }
            if (tp != PageModel.CurrentPage)
            {
                var tagson = new TagBuilder("a");
                if (PageModel.CategoryName != null)
                {
                    tagson.Attributes["href"] = urlHelper.Action(PageAction, new { page = PageModel.CurrentPage + 1 , category = PageModel.CategoryName });
                }
                else
                {
                    tagson.Attributes["href"] = urlHelper.Action(PageAction, new { page = PageModel.CurrentPage + 1 });
                }
               
                tagson.InnerHtml.Append(">>");
                tagson.AddCssClass("btn btn-primary btn-circle ml-1");
                result.InnerHtml.AppendHtml(tagson);
            }
          


            output.Content.AppendHtml(result.InnerHtml);
        }

    }
}
