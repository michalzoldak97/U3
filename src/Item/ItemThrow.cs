using System.Collections;
using UnityEngine;

namespace U3.Item
{
    public class ItemThrow : MonoBehaviour
    {
        private ItemMaster itemMaster;

        private void OnEnable()
        {
            itemMaster = GetComponent<ItemMaster>();

            itemMaster.EventThrow += StartThrowItem;
        }
        private void OnDisable()
        {
            itemMaster.EventThrow -= StartThrowItem;
        }

        private IEnumerator ThrowItem(Transform origin)
        {
            yield return new WaitForEndOfFrame();

            if (TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(origin.forward *
                    itemMaster.ItemSettings.ThrowForce, ForceMode.Impulse);
            }
        }

        private void StartThrowItem(Transform origin)
        {
            StartCoroutine(ThrowItem(origin));
        }
    }
}
