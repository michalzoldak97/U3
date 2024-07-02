using U3.Log;
using UnityEngine;

namespace U3.Global.Rendering 
{
    public class SceneCameraManager : MonoBehaviour
    {
        public static SceneCameraManager Instance;
        public Camera MainCamera => mainCamera;

        [SerializeField] private ToggleableRenderrerFeature[] toggleableRenderrerFeatures;

        [SerializeField] private SceneCamera[] sceneCameras;

        private Camera mainCamera;

        public delegate void SceneCameraEventHandler(Camera cam);
        public event SceneCameraEventHandler EventMainCameraChanged;

        private void ChangeCameraFeatures(SceneCamera sceneCamera, bool toState)
        {
            string[] featureCodes = toState ? sceneCamera.RendererFeaturesToActivateCodes : sceneCamera.RendererFeaturesToDeactivateCodes;

            foreach (string featureCode in featureCodes)
            {
                foreach (ToggleableRenderrerFeature feature in toggleableRenderrerFeatures)
                {
                    if (feature.rendererFeatureGroupCode == featureCode && (feature.isActive != toState))
                    {
                        feature.rendererFeature.SetActive(toState);
                        feature.isActive = toState;
                    }
                }
            }
        }

        private void DisableSceneCameras()
        {
            foreach (SceneCamera camera in sceneCameras)
            {
                camera.ChangeObjectState(false);
                ChangeCameraFeatures(camera, false);
            }
        }

        private void CameraStateActions(SceneCamera sceneCamera)
        {
            DisableSceneCameras();
            ChangeCameraFeatures(sceneCamera, true);
            sceneCamera.ChangeObjectState(true);
            mainCamera = Camera.main;
            EventMainCameraChanged?.Invoke(mainCamera);
        }

        public void EnableSceneCamera(string cameraCode)
        {
            foreach (SceneCamera camera in sceneCameras)
            {
                if (camera.CameraCode == cameraCode)
                {
                    CameraStateActions(camera);
                    return;
                }
            }

            GameLogger.Log(new GameLog(Log.LogType.Warning, $"Attempt to disable a scene camera with missing code {cameraCode}"));
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            mainCamera = Camera.main;
        }
    }
}