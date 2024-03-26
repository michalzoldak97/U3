using UnityEngine;
using UnityEngine.SceneManagement;

namespace U3.Global.GameScene
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void SwotchScene(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
