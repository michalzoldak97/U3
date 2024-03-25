using UnityEngine.Rendering.Universal;

namespace U3.Global.Rendering
{
    [System.Serializable]
    public class ToggleableRenderrerFeature
    {
        public bool isActive;
        public string rendererFeatureGroupCode;
        public ScriptableRendererFeature rendererFeature;
    }
}
