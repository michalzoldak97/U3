using System.Runtime.CompilerServices;

namespace U3.Log
{
    public class GameLog
    {
        public int LineNumber { get; }
        public LogType LogType { get; }
        public string LineName { get; }
        public string LineFilePath { get; }
        public string Message { get; }

        public string GetFullMessage() => $"{Message} at line {LineNumber}, member {LineName}, file {LineFilePath}";

        public GameLog (LogType logType,
            string message,
            [CallerLineNumber] int lineNumber = default,
            [CallerMemberName] string lineName = default,
            [CallerFilePath] string lineFilePath = default)
        {
            LineNumber = lineNumber;
            LogType = logType;
            LineName = lineName;
            LineFilePath = lineFilePath;
            Message = $"{GameLogger.LogTypePerfixes[logType]}: {message}";
        }
    }
}
