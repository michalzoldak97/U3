using U3.Item;
using UnityEngine;

namespace U3.Player.Inventory
{
    internal interface IItemDetails
    {
        public GameObject UIObject { get; }
        public void SetItemSettings(ItemSettings itemSettings);
    }
}
