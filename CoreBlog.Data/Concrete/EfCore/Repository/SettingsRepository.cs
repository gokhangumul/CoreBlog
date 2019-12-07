using CoreBlog.Data.Abstract;
using CoreBlog.Data.Concrete.EfCore.Context;
using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBlog.Data.Concrete.EfCore.Repository
{
    public class SettingsRepository:GenericRepository<Setting>,ISettingsRepository
    {
        public SettingsRepository(BlogContext blogContext) : base(blogContext)
        {

        }
    }
}
