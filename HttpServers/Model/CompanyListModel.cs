using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class CompanyListModel
    {
        public string status
        {
            get;set;
        }
        public string msg
        {
            get;set;
        }
        public List<CompanyModel> companyList
        {
            get;set;
        }
    }
}
