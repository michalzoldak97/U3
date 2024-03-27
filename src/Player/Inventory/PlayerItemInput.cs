using U3.Input;
using U3.Inventory;
using U3.Item;
using U3.Weapon;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PlayerItemInput : MonoBehaviour
    {
        private FireInputOrigin m_inputOrigin;
        private IAmmoStore m_ammoStore;
        private InventoryMaster inventoryMaster;

        private void GenerateInputOrigin()
        {
            int id = transform.GetInstanceID();
            string name = transform.name;
            m_inputOrigin = new FireInputOrigin(id, name);
        }

        private void OnEnable()
        {
            GenerateInputOrigin();

            m_ammoStore = GetComponent<IAmmoStore>();
            inventoryMaster = GetComponent<InventoryMaster>();

            PlayerInputManager.HumanoidInputActions.EventFireDown += () => CallPlayerInputOnItem(ItemInputType.FireDown);
            PlayerInputManager.HumanoidInputActions.EventFireUp += () => CallPlayerInputOnItem(ItemInputType.FireUp);
            PlayerInputManager.HumanoidInputActions.EventAimDown += () => CallPlayerInputOnItem(ItemInputType.AimDown);
            PlayerInputManager.HumanoidInputActions.EventAimUp += () => CallPlayerInputOnItem(ItemInputType.AimUp);
            PlayerInputManager.HumanoidInputActions.EventReload += () => CallPlayerInputOnItem(ItemInputType.Reload);
        }

        private void OnDisable()
        {
            PlayerInputManager.HumanoidInputActions.EventFireDown -= () => CallPlayerInputOnItem(ItemInputType.FireDown);
            PlayerInputManager.HumanoidInputActions.EventFireUp -= () => CallPlayerInputOnItem(ItemInputType.FireUp);
            PlayerInputManager.HumanoidInputActions.EventAimDown -= () => CallPlayerInputOnItem(ItemInputType.AimDown);
            PlayerInputManager.HumanoidInputActions.EventAimUp -= () => CallPlayerInputOnItem(ItemInputType.AimUp);
            PlayerInputManager.HumanoidInputActions.EventReload -= () => CallPlayerInputOnItem(ItemInputType.Reload);
        }

        private void CallPlayerInputOnItem(ItemInputType inputType)
        {
            if (inventoryMaster.SelectedItem == null)
                return;

            if (inventoryMaster.SelectedItem.TryGetComponent(out IItemInputProvider inputProvider))
            {
                if (inputType == ItemInputType.FireDown)
                    inputProvider.CallEventFireDownCalled(m_inputOrigin);
                else if (inputType == ItemInputType.FireUp)
                    inputProvider.CallEventFireUpCalled(m_inputOrigin);
                else if (inputType == ItemInputType.AimDown)
                    inputProvider.CallEventAimDownCalled();
                else if (inputType == ItemInputType.AimUp)
                    inputProvider.CallEventAimUpCalled();
                else if (inputType == ItemInputType.Reload)
                    inputProvider.CallEventReloadCalled(m_ammoStore);
            }
        }
    }
}
