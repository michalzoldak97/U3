using TMPro;
using U3.Item;
using UnityEngine;

namespace U3.Player.Inventory.ItemDetails
{
    public class BasicItemDetails : MonoBehaviour, IItemDetails
    {
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemType;
        [SerializeField] private TMP_Text itemDescription;

        private ItemSettings itemSettings;

        public GameObject UIObject => transform.gameObject;

        private void SetUpDetails()
        {
            itemName.text = itemSettings.ToItemName;
            itemType.text = itemSettings.ItemType.ToString();
            itemDescription.text = itemSettings.ItemDescription;
        }

        public void SetItemSettings(ItemSettings itemSettings)
        {
            this.itemSettings = itemSettings;
            SetUpDetails();
        }
    }
}
