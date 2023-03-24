using UnityEngine;

namespace U3
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
