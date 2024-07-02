using System.Collections;
using U3.Destructible;
using UnityEngine;

namespace U3.Benchmark.Utility
{
    public class FrameCounter : MonoBehaviour
    {
        [SerializeField] private int secToCount;

        private int secElapsed;
        private int frameCountSec;
        private int frameCountAll;
        private WaitForSeconds waitSec = new WaitForSeconds(1f);

        private IEnumerator CountFrames()
        {
            for (int i = 0; i < secToCount; i++)
            {
                yield return waitSec;
                Debug.Log($"Frame rate = {frameCountSec}");
               /* Debug.Log($"Played in a second {HitEffectManager.Instance.dbgEffectsPlayed} active in a second {HitEffectManager.Instance.dbgEffectsActive / frameCountSec}");
                HitEffectManager.Instance.dbgEffectsPlayed = 0;
                HitEffectManager.Instance.dbgEffectsActive = 0;*/
                frameCountSec = 0;
                secElapsed++;
            }
            Debug.Log($"AVG Frame rate = {frameCountAll / secElapsed}");
        }

        private void Start()
        {
            StartCoroutine(CountFrames());
        }

        private void Update()
        {
            frameCountSec++;
            frameCountAll++;
        }
    }
}
