using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Commom
{
    public class ModifyUsersModel
    {
        public string ID
        {
            get; set;
        }
        public string userName
        {
            get;set;
        }
        public string password
        {
            get; set;
        }
        public string realName
        {
            get; set;
        }
        public string note
        {
            get; set;
        }
        public string role
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
        public string company
        {
            get;set;
        }
        public string phone
        {
            get;set;
        }
    }
}
