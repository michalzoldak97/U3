namespace U3.Player
{
    /// <summary>
    /// Dummy class to address a need for API communication in the future
    /// </summary>
    public class PlayerAPIMaster
    {
        public delegate void PlayerAPIEventsHandler();
        public event PlayerAPIEventsHandler EventPlayerSettingsUpdate;

        public void CallEventPlayerSettingsUpdate()
        {
            EventPlayerSettingsUpdate?.Invoke();
        }
    }
}
