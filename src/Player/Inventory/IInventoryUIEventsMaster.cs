using UnityEngine;

namespace U3.Player.Inventory
{
    public interface IInventoryUIEventsMaster
    {
        public GameObject DetailsParent { get; }

        public Canvas MainCanvas { get; }
        public void CallEventOnItemButtonDrop(IItemButton itemButton, RectTransform buttonTransform);

        public void CallEventAssignItemToFreeSlot(Transform item);
        public void CallEventItemFocused(Transform item);
        public void CallEventItemUnfocused(Transform item);
    }
}
