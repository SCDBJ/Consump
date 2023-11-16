using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Commom
{
    public class InitConfig
    {
        public static string Init()
        {
            string configPath = Environment.CurrentDirectory + @"\config.ini";
            if (!File.Exists(configPath))
            {
                return "没有找到配置文件！";
            }
            string tmp = OperationFile.ReadIniData("ConfigData", "ApiName", "", configPath).Trim();
            if (string.IsNullOrWhiteSpace(tmp))
            {
                return "没有找到【ApiName】配置项！";
            }
            return tmp.Trim();
        }
    }
}
