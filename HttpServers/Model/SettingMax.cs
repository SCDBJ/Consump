using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    [SugarTable("SettingMax")]
    public class SettingMax
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
        public int ID
        {
            get; set;
        }
        [SugarColumn(ColumnName = "watermax",Length =50)]
        public string watermax
        {
            get; set;
        }
        [SugarColumn(ColumnName = "electricmax", Length = 50)]
        public string electricmax
        {
            get; set;
        }
    }
}
