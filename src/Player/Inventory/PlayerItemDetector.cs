using UnityEngine;

namespace U3.Player.Inventory
{
    public class PlayerItemDetector : MonoBehaviour
    {
        [SerializeField] private Transform fpsCamera;

        private bool isItemInRange;
        private int ignorePlayerLayerMask;
        private float nextCheck, checkRate;
        private Vector2 labelDimensions;
        private LayerMask itemLayer;
        private Transform itemInRange;
        private Rect labelRect;
        private GUIStyle labelStyle = new();

        private void SetInit()
        {
            InventorySettings inventorySettings = GetComponent<PlayerMaster>().PlayerSettings.Inventory;
            checkRate = inventorySettings.ItemCheckRate;
            labelDimensions = inventorySettings.LabelDimensions;
            labelRect = new Rect
                (
                    Screen.width / 2f - labelDimensions.x,
                    Screen.height / 2f,
                    labelDimensions.x,
                    labelDimensions.y
                );
            labelStyle.fontSize = inventorySettings.LabelFontSize;
            labelStyle.normal.textColor = inventorySettings.LabelColor;

            itemLayer = 1 << LayerMask.NameToLayer("Item");

            // mask to exclude player layer from check for item visibility
            LayerMask playerLayer = 1 << LayerMask.NameToLayer("Player");
            ignorePlayerLayerMask = ~playerLayer;
        }
    }
}
