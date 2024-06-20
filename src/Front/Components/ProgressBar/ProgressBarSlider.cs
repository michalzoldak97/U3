using U3.Log;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Front.Components.ProgressBar
{
    public class ProgressBarSlider : MonoBehaviour, IProgressBar
    {
        [SerializeField] private Slider progressBar;

        public bool AddProgress(int percent)
        {
            if (progressBar.value >= progressBar.maxValue)
                return false;

            if (percent < 1)
            {
                GameLogger.Log(new GameLog(Log.LogType.Error, $"Progress bar can be increaesed by value not less than one, trying to increase by {percent}"));
                return false;
            }

            progressBar.value = progressBar.value + percent > progressBar.maxValue ? progressBar.maxValue : progressBar.value + percent;

            return true;
        }

        public void ResetProgeress()
        {
            progressBar.value = 0;
        }

        public void SetFull()
        {
            progressBar.value = progressBar.maxValue;
        }

        private void OnEnable()
        {
            progressBar.value = 0;
            progressBar.maxValue = 100;
        }
    }
}