using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class IncomeWModel
    {
        public string incomeType
        {
            get; set;
        }
        public decimal incomeAmount
        {
            get; set;
        }
        public DateTime incomeTime
        {
            get; set;
        }
        public string incomeNote
        {
            get; set;
        }
    }
}
