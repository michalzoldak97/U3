using UnityEngine;

namespace U3.Player.Inventory.UI
{
    public interface IDropContainer
    {
        public bool IsDropAvailable();
        public void OnDropAttempt(RectTransform t);
    }
}
