using U3.Global.Rendering;
using UnityEngine;

namespace U3.Damageable
{
    public class DamageTextManager : MonoBehaviour
    {
        [SerializeField] private DamageTextSettings dmgTextSettings;

        public const string DamageTextPoolCode = "DamageText";
        public DamageTextSettings DamageTextSettings => dmgTextSettings;
        public WaitForSeconds LoopDelay { get; private set; }
        public WaitForSeconds WaitLock { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public static DamageTextManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            LoopDelay = new WaitForSeconds(dmgTextSettings.LoopDelayDurationSec);
            WaitLock = new WaitForSeconds(dmgTextSettings.LoopDelayDurationSec * dmgTextSettings.TextAnimationIterations);
            MainCameraTransform = Camera.main.transform;
        }

        private void OnEnable()
        {
            SceneCameraManager.Instance.EventMainCameraChanged += SetMainCameraTransform;
        }

        private void OnDisable()
        {
            SceneCameraManager.Instance.EventMainCameraChanged -= SetMainCameraTransform;
        }

        private void SetMainCameraTransform(Camera cam) => MainCameraTransform = cam.transform;
    }
}
