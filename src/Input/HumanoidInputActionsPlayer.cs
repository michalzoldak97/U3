using System.Collections.Generic;
using System.IO;
using U3.Global.Helper;
using UnityEngine.InputSystem;
using UnityEngine;

namespace U3.Input
{
    public class HumanoidInputActionsPlayer : MonoBehaviour
    {
        [SerializeField] private string keyboardRecordFilePath;
        [SerializeField] private string mouseRecordFilePath;

        private int keyboardIndex, mouseIndex;

        private readonly Dictionary<int, KeyboardInput> keyboardInputs = new();
        private readonly Dictionary<int, MouseInput> mouseInputs = new();

        private void LoadInputs()
        {
            string keyboardJSON = File.ReadAllText(keyboardRecordFilePath);
            KeyboardInput[] keyboardData = Helper.JSONToArray<KeyboardInput>(keyboardJSON);
            foreach (KeyboardInput k in keyboardData)
            {
                keyboardInputs.Add(k.Index, k);
            }

            string mouseJSON = File.ReadAllText(mouseRecordFilePath);
            MouseInput[] mouseData = Helper.JSONToArray<MouseInput>(mouseJSON);
            foreach (MouseInput m in mouseData)
            {
                mouseInputs.Add(m.Index, m);
            }
        }

        private void RunKeyboardData(KeyboardInput keyboardInput)
        {
            PlayerInputManager.HumanoidInputActions.SetMove(keyboardInput.Move);

            if (keyboardInput.RunStart)
                PlayerInputManager.HumanoidInputActions.CallEventRunStart(new InputAction.CallbackContext());

            if (keyboardInput.RunFinish)
                PlayerInputManager.HumanoidInputActions.CallEventRunFinish(new InputAction.CallbackContext());

            if (keyboardInput.Jump)
                PlayerInputManager.HumanoidInputActions.CallEventJump(new InputAction.CallbackContext());

            if (keyboardInput.ItemInteract)
                PlayerInputManager.HumanoidInputActions.CallEventItemInteract(new InputAction.CallbackContext());

            if (keyboardInput.ItemThrow)
                PlayerInputManager.HumanoidInputActions.CallEventItemThrow(new InputAction.CallbackContext());

            if (keyboardInput.ToggleInventory)
                PlayerInputManager.HumanoidInputActions.CallEventToggleInventory(new InputAction.CallbackContext());

            if (keyboardInput.Aim)
                PlayerInputManager.HumanoidInputActions.CallEventAim(new InputAction.CallbackContext());

            if (keyboardInput.Shoot)
                PlayerInputManager.HumanoidInputActions.CallEventShoot(new InputAction.CallbackContext());

            if (keyboardInput.Reload)
                PlayerInputManager.HumanoidInputActions.CallEventReload(new InputAction.CallbackContext());

            if (keyboardInput.ChangeWeaponMode)
                PlayerInputManager.HumanoidInputActions.CallEventChangeWeaponMode(new InputAction.CallbackContext());

            if (keyboardInput.ChangeActiveInventorySlot1)
                PlayerInputManager.HumanoidInputActions.CallEventChangeActiveInventorySlot1(new InputAction.CallbackContext());

            if (keyboardInput.ChangeActiveInventorySlot2)
                PlayerInputManager.HumanoidInputActions.CallEventChangeActiveInventorySlot2(new InputAction.CallbackContext());

            if (keyboardInput.ChangeActiveInventorySlot3)
                PlayerInputManager.HumanoidInputActions.CallEventChangeActiveInventorySlot3(new InputAction.CallbackContext());

            if (keyboardInput.ChangeActiveInventorySlot4)
                PlayerInputManager.HumanoidInputActions.CallEventChangeActiveInventorySlot4(new InputAction.CallbackContext());

            if (keyboardInput.ChangeActiveInventorySlot5)
                PlayerInputManager.HumanoidInputActions.CallEventChangeActiveInventorySlot5(new InputAction.CallbackContext());

            if (keyboardInput.ChangeActiveInventorySlot6)
                PlayerInputManager.HumanoidInputActions.CallEventChangeActiveInventorySlot6(new InputAction.CallbackContext());

            if (keyboardInput.ChangeActiveInventorySlot7)
                PlayerInputManager.HumanoidInputActions.CallEventChangeActiveInventorySlot7(new InputAction.CallbackContext());
        }

        private void PlayKeyboard()
        {
            if (!keyboardInputs.ContainsKey(keyboardIndex))
            {
                Debug.Log("Finished playing keyboard");
                return;
            }

            RunKeyboardData(keyboardInputs[keyboardIndex]);
            keyboardIndex++;
        }

        private void RunMouseData(MouseInput mouseInput)
        {
            PlayerInputManager.HumanoidInputActions.SetMouseX(mouseInput.MouseX);
            PlayerInputManager.HumanoidInputActions.SetMouseX(mouseInput.MouseY);
        }

        private void PlayMouse()
        {
            if (!mouseInputs.ContainsKey(mouseIndex))
            {
                Debug.Log("Finished playing mouse");
                return;
            }

            RunMouseData(mouseInputs[mouseIndex]);
            mouseIndex++;
        }

        private void Start()
        {
            LoadInputs();
        }

        private void FixedUpdate()
        {
            PlayKeyboard();
        }

        private void Update()
        {
            PlayMouse();
        }
    }
}
