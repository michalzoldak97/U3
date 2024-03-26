using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace U3.Global.Helper
{
    public static class ApplicationState
    {
        #region Static

        public static bool IsQuitting { get; private set; }

        public static bool IsSceneUnloading { get; private set; }

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void EnterPlayMode(EnterPlayModeOptions options)
        {
            IsQuitting = false;
            IsSceneUnloading = false;
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart()
        {
            IsQuitting = false;
            IsSceneUnloading = false;

            Application.quitting += Quit;
            SceneManager.activeSceneChanged += UnloadScene;
        }

        private static void Quit()
        {
            IsQuitting = true;
        }

        private static void UnloadScene(Scene a, Scene b)
        {
            IsSceneUnloading = true;
        }

        #endregion
    }
}
