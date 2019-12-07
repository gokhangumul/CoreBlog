using CoreBlog.Business.Abstract;
using CoreBlog.Data.Abstract;
using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBlog.Business.Concrete
{
    public class ImageManager:GenericManager<Image>,IImageService
    {
        private readonly IImageRepository imagerepo;

        public ImageManager(IImageRepository imagerepo):base(imagerepo)
        {
            this.imagerepo = imagerepo ?? throw new ArgumentNullException(nameof(imagerepo));
        }
    }
}
