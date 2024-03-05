using U3.Player.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U3.Player.Inventory
{
    public class DraggableItem : MonoBehaviour, IUIScreenStateDependent, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IItemButton itemButton;
        private IInventoryUIEventsMaster uIEventsMaster;
        private RectTransform rTransform;

        public void SetUpDraggableItem(IInventoryUIEventsMaster uIEventsMaster)
        {
            this.uIEventsMaster = uIEventsMaster;

            itemButton = GetComponent<ItemButton>();
            rTransform = GetComponent<RectTransform>();
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            Debug.Log("OnInitializePotentialDrag");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            rTransform.SetParent(uIEventsMaster.MainCanvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            rTransform.anchoredPosition += eventData.delta / uIEventsMaster.MainCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            uIEventsMaster.CallEventOnItemButtonDrop(itemButton, rTransform);
        }

        public void OnUIScreenDisabled()
        {
            if (rTransform.parent != itemButton.ParentArea.ItemParentTransform)
                rTransform.SetParent(itemButton.ParentArea.ItemParentTransform);
        }
    }
}
