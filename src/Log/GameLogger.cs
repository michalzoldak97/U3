using System.Text;
using U3.Global.Config;
using UnityEngine;

namespace U3.Log
{
    public static class GameLogger
    {
        public static void Log(LogType logType, string msg)
        {
            if (!GameConfig.EnableLogs)
                return;

            StringBuilder logMsg = new();

            switch (logType)
            {
                case LogType.Error:
                    logMsg.Append("ERROR: ");
                    break;

                case LogType.Warning:
                    logMsg.Append("WARNING: ");
                    break;

                case LogType.Notification:
                    logMsg.Append("NOTIFICATION: ");
                    break;

                default:
                    logMsg.Append("MESSAGE: ");
                    break;
            }

            logMsg.Append(msg);
            Debug.Log(logMsg.ToString());
        }
    }
}
