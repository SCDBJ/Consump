using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class CategoryAddModel
    {
        public int categoryId
        {
            get; set;
        }
        public string categoryName
        {
            get; set;
        }
        public string categoryType
        {
            get; set;
        }
        public DateTime createTime
        {
            get; set;
        }
    }
}
