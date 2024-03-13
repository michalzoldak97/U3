using System.Collections.Generic;
using U3.Global.Config;
using UnityEngine;

namespace U3.Log
{
    public static class GameLogger
    {
        public static readonly Dictionary<LogType, string> LogTypePerfixes = new()
        {
            { LogType.Error, "ERROR"},
            { LogType.Warning, "WARNING"},
            { LogType.Notification, "NOTIFICATION"}
        };
        public static void Log(GameLog gameLog)
        {
            if (!GameConfig.GameConfigSettings.EnableLogs)
                return;

            if (gameLog.LogType == LogType.Error)
            {
                Debug.LogException(new System.Exception(gameLog.GetFullMessage()));
                Debug.Break();
                return; // TODO: send log to the server 
            }

            Debug.Log(gameLog.GetFullMessage()); // TODO: send log to the server
        }
    }
}
