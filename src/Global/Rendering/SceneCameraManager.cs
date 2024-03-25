using System.Collections.Generic;
using U3.Log;
using UnityEngine;

namespace U3.Global.Rendering 
{
    public class SceneCameraManager : MonoBehaviour
    {
        public static SceneCameraManager instance;

        [SerializeField] private ToggleableRenderrerFeature[] toggleableRenderrerFeatures;
        [SerializeField] private SceneCamera[] sceneCameras;

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
            if (instance == null)
                instance = this;
        }
    }
}