using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class ConsumpAllModel
    {
        public int consumpId
        {
            get; set;
        }
        public string consumpType
        {
            get; set;
        }
        public decimal consumpAmount
        {
            get; set;
        }
        public string consumpNote
        {
            get; set;
        }
        public DateTime consumpTime
        {
            get; set;
        }
        public DateTime createTime
        {
            get; set;
        }
    }
}
