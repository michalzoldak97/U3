using U3.Log;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Front.Components.ProgressBar
{
    public class ProgressBarButtons : MonoBehaviour, IProgressBar
    {
        [SerializeField] private Color32 startColor;
        [SerializeField] private Color32 completedColor;
        [SerializeField] private Image[] progressButtonBackgrounds;

        private int completion;

        private int GetNumOfButtonsToUpdate()
        {
            float step = 100.0f / progressButtonBackgrounds.Length;
            return (int)Mathf.Floor(completion / step);
        }

        public bool AddProgress(int percent)
        {
            if (completion >= 100)
                return false;

            if (percent < 1)
            {
                GameLogger.Log(new GameLog(Log.LogType.Error, $"Progress bar can be increaesed by value not less than one, trying to increase by {percent}"));
                return false;
            }

            completion = completion + percent > 100 ? 100 : completion + percent;

            for (int i = 0; i < GetNumOfButtonsToUpdate(); i++)
            {
                if (progressButtonBackgrounds[i].color != completedColor)
                    progressButtonBackgrounds[i].color = completedColor;
            }

            return true;
        }

        public void ResetProgeress()
        {
            completion = 0;
            for (int i = 0; i < progressButtonBackgrounds.Length; i++)
            {
                progressButtonBackgrounds[i].color = startColor;
            }
        }

        private void Start()
        {
            ResetProgeress();
        }
    }
}