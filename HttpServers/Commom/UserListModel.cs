using HttpServers.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Commom
{
    public class UserListModel
    {
        public string status
        {
            get;set;
        }
        public string msg
        {
            get; set;
        }
        public List<UserManager> userList
        {
            get;set;
        }
    }
}
