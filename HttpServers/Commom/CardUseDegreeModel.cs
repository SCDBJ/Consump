using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Commom
{
    public class CardUseDegreeModel
    {
        public string deviceNo
        {
            get;set;
        }
        public string cardID
        {
            get;set;
        }
        public string electricDegree
        {
            get;set;
        }
        public string waterDegree
        {
            get;set;
        }
        public DateTime endTime
        {
            get;set;
        }
    }
}
