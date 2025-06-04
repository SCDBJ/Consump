using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class ConsumpWModel
    {
        public string consumpCategory
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
    }
}
