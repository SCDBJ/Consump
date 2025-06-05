using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class ConsumpStatisticeModel
    {
        public string consumpType
        {
            get;set;
        }
        public int numbers
        {
            get; set;
        }
        public decimal sumamount
        {
            get; set;
        }
        public decimal avgamount
        {
            get; set;
        }
    }
}
