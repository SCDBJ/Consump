using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model.WebSite
{
    public class WebSiteModel
    {
        public string websiteId
        {
            get; set;
        }
        public string websiteName
        {
            get; set;
        }
        public string websiteHome
        {
            get; set;
        }
        public string websiteDetail
        {
            get; set;
        }
        public string websiteCategory
        {
            get; set;
        }
        public string contentTitle
        {
            get; set;
        }
        public string websiteRemark
        {
            get; set;
        }
        public string commonUse
        {
            get;set;
        }
        public string websiteDefault
        {
            get; set;
        }
        public DateTime createTime
        {
            get; set;
        }
    }
}
