using U3.Weapon;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryAmmoStoreSetter : MonoBehaviour
    {
        private IAmmoStore m_ammoStore;
        private InventoryMaster inventoryMaster;

        private void OnEnable()
        {
            m_ammoStore = GetComponent<IAmmoStore>();
            inventoryMaster = GetComponent<InventoryMaster>();

            inventoryMaster.EventItemSelected += (Transform item) => SetAmmoStore(m_ammoStore);
        }

        private void OnDisable()
        {
            inventoryMaster.EventItemSelected -= (Transform item) => SetAmmoStore(m_ammoStore);
        }

        private void SetAmmoStore(IAmmoStore ammoStore)
        {
            if (inventoryMaster.SelectedItem == null)
                return;

            if (inventoryMaster.SelectedItem.TryGetComponent(out IWeaponAmmoConsumer ammoConsumer))
                ammoConsumer.SetAmmoStore(ammoStore);
        }
    }
}
