using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.Helpers
{
    public class ImageCheck:IDisposable
    {
        public ImageCheck ()
        {
        }
        public bool CheckImageUzan(IFormFile images)
        {
            bool check = true;
            if (images.ContentType != "image/jpg" && images.ContentType != "image/jpeg" && images.ContentType != "image/png" && images.ContentType != "image/JPG")
            {
                check = false;
            }
            return check;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
