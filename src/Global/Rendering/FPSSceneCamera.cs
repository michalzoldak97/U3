using UnityEngine;

namespace U3.Global.Rendering
{
    public class FPSSceneCamera : SceneCamera
    {
        private Camera m_camera;
        private AudioListener m_audioListener;
        public override void ChangeObjectState(bool toState)
        {
            isChangeByMethod = true;

            m_camera.enabled = toState;
            m_audioListener.enabled = toState;
        }

        private void Start()
        {
            m_camera = GetComponent<Camera>();
            m_audioListener = GetComponent<AudioListener>();
        }
    }
}
