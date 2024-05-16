using System.Collections;
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
