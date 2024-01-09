using UnityEngine;

namespace U3.Player.UI
{
    public class ItemListUI : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log("I m activated");
        }

        private void OnDisable()
        {
            Debug.Log("Bye");
        }
    }
}
