using UnityEngine;

namespace U3.Global.Config
{
    public class InEditorConfigLoader : MonoBehaviour
    {
        [SerializeField] private GameConfigSettings gameConfig;

        private void Awake()
        {
            GameConfig.SetGameConfigSettings(gameConfig);
            GetComponent<StaticInitializer>().Initialize();
        }
    }
}
