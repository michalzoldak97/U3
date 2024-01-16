using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class InventoryUIEventsManager : MonoBehaviour
    {
        private PlayerInventoryMaster inventoryMaster;

        private void SetInit()
        {
            if (TryGetComponent(out PlayerInventoryMaster playerInventoryMaster))
            {
                inventoryMaster = playerInventoryMaster;
            }
            else
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"There is no PlayerInventoryMaster on the {transform.name} object, required by the {name}"));
            }
        }

        private void OnEnable()
        {
            if (inventoryMaster == null)
                SetInit();

            inventoryMaster.EventOnItemButtonDrop += OnItemButtonDrop;
        }

        private void OnDisable()
        {
            inventoryMaster.EventOnItemButtonDrop -= OnItemButtonDrop;
        }

        private void OnItemButtonDrop(IItemButton itemButton, IInventoryDropArea dropArea)
        {
            if (dropArea == itemButton.ParentArea)
                return;

            // if drop area == null => determine drop area from rect transform

            // if drop area is button and parent area is slot and drop are button type is accepted by the slot do the swap

            // check if new area can accept the button

            // inform previous area that item button has been removed

            // inform new area button was assigned

            // inform inventory 
        }
    }
}