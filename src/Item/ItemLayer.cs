using UnityEngine;

namespace U3.Item
{
    public class ItemLayer : MonoBehaviour
    {
        private int originalLayer;

        private ItemMaster itemMaster;

        private void OnEnable()
        {
            itemMaster = GetComponent<ItemMaster>();

            itemMaster.EventAddedToInventory += ChangeLayerOnAdd;
            itemMaster.EventRemovedFromInventory += ChangeLayerOnRemove;
        }

        private void OnDisable()
        {
            itemMaster.EventAddedToInventory -= ChangeLayerOnAdd;
            itemMaster.EventRemovedFromInventory -= ChangeLayerOnRemove;
        }

        private void ChangeLayer(int newLayer)
        {
            gameObject.layer = newLayer;
            foreach (Transform child in transform.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = newLayer;
            }
        }

        private void ChangeLayerOnAdd()
        {
            ChangeLayer(LayerMask.NameToLayer(itemMaster.ItemSettings.ToLayerName));
        }

        private void ChangeLayerOnRemove()
        {
            ChangeLayer(originalLayer);
        }

        private void Start()
        {
            originalLayer = gameObject.layer;
        }
    }
}
