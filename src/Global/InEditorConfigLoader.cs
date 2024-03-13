using U3.Global.Config;
using UnityEngine;

namespace U3.Global
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
