namespace U3.Global.Config
{
    public static class GameConfig
    {
        public static bool EnableLogs { get; private set; }
        public static void Init()
        {
            EnableLogs = true;
        }
    }
}
