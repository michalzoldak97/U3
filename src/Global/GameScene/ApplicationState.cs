using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace U3.Global.GameScene
{
    public static class ApplicationState
    {
        #region Static

        public static bool IsQuitting { get; private set; }

        public static bool IsSceneSwitching { get; set; }

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void EnterPlayMode(EnterPlayModeOptions options)
        {
            IsQuitting = false;
            IsSceneSwitching = false;
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart()
        {
            IsQuitting = false;
            IsSceneSwitching = false;

            Application.quitting += Quit;
            SceneManager.sceneLoaded += SceneSwitchFinished;
        }

        private static void Quit()
        {
            IsQuitting = true;
        }

        private static void SceneSwitchFinished(Scene _, LoadSceneMode mode)
        {
            IsSceneSwitching = false;
        }

        #endregion
    }
}
