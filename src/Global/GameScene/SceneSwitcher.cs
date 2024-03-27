using U3.Global.Helper;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace U3.Global.GameScene
{
    public class SceneSwitcher : MonoBehaviour
    {
        public static SceneSwitcher Instance;

        public void SwitchScene(int sceneIndex)
        {
            ApplicationState.IsSceneSwitching = true;
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}
