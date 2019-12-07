using CoreBlog.Data.Abstract;
using CoreBlog.Data.Concrete.EfCore.Context;
using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBlog.Data.Concrete.EfCore.Repository
{
    public class ImageRepository : GenericRepository<Image>,IImageRepository
    {
        public ImageRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
