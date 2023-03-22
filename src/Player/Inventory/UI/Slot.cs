using TMPro;
using UnityEngine;

namespace U3.Player.Inventory.UI
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private TMP_Text slotName;
        
        public void SetSlotName(string toSet)
        {
            slotName.text = toSet;
        }
    }
}
