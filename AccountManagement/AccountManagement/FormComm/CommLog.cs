using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.FormComm
{
    class CommLog
    {
        public static ILog loger = LogManager.GetLogger("AM_Log");//这里的 loginfo 和 log4net.config 里的名字要一样


    }
}
