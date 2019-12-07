using CoreBlog.Business.Abstract;
using CoreBlog.Data.Abstract;
using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBlog.Business.Concrete
{
    public class SettingsManager:GenericManager<Setting>,ISettingsService
    {
        private readonly ISettingsRepository settingsRepository;

        public SettingsManager(ISettingsRepository settingsRepository):base(settingsRepository)
        {
            this.settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
        }
    }
}
