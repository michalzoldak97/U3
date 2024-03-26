using UnityEngine;
using U3.Global.Helper;

namespace U3.Global.Rendering
{
    public class SceneCamera : ObjectStateProxy
    {
        [SerializeField] private string cameraCode;
        [SerializeField] private string[] rendererFeaturesToActivateCodes;
        [SerializeField] private string[] rendererFeaturesToDeactivateCodes;

        public string CameraCode => cameraCode;
        public string[] RendererFeaturesToActivateCodes => rendererFeaturesToActivateCodes;
        public string[] RendererFeaturesToDeactivateCodes => rendererFeaturesToDeactivateCodes;
    }
}
