namespace U3.Input
{
    internal static class HumanoidInputActionsFactory
    {
        internal static HumanoidInputActions GetInputActions(string actionsCode) => actionsCode switch
        {
            "test" => new PlayableHumanoidInputActions(),
            _ => new HumanoidInputActions()
        };
    }
}
