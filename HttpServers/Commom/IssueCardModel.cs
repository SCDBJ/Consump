using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Commom
{
    public class IssueCardModel
    {
        public int ID
        {
            get;set;
        }
        public string cardID
        {
            get;set;
        }
        public string userName
        {
            get; set;
        }
        public string phonenum
        {
            get; set;
        }
        public string companyName
        {
            get; set;
        }
        public string shipCode
        {
            get; set;
        }
        public int inUse
        {
            get; set;
        }
        public DateTime addTime
        {
            get; set;
        }
        public DateTime unUseTime
        {
            get; set;
        }
        public string operation
        {
            get; set;
        }
    }
}
