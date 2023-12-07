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
            if (!GameConfig.EnableLogs)
                return;

            Debug.Log($"{gameLog.GetFullMessage()}"); // TODO: send log to the server
        }
    }
}
