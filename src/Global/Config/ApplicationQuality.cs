using UnityEngine;

namespace U3.Global.Config
{
    public class ApplicationQuality : MonoBehaviour
    {
        [SerializeField] private int targetFrames;

        private void Start()
        {
            Application.targetFrameRate = targetFrames;
        }
    }
}
