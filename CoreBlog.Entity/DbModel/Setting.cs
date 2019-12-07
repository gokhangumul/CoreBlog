using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBlog.Entity.DbModel
{
    public class Setting
    {
        public int Id { get; set; }
        public string SettingName { get; set; }
        public string About { get; set; }
        public string MainHeader { get; set; }
        public string BlogName { get; set; }
        public string MainSubHeader { get; set;}
        public string MainHeaderImage { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string UniqKey { get; set; }
        
        

    }
}
