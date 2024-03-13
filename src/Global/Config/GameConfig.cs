namespace U3.Global.Config
{
    public static class GameConfig
    {
        public static GameConfigSettings GameConfigSettings { get; private set; }

        public static void SetGameConfigSettings(GameConfigSettings toSet) => GameConfigSettings = toSet;
    }
}
