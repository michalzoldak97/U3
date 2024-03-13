using System.Collections.Generic;
using System.IO;
using U3.Global.Helper;
using UnityEngine;

namespace U3.Input
{
    [System.Serializable]
    internal struct KeyboardInput
    {
        [SerializeField] public int Index;
        [SerializeField] public Vector2 Move;
        [SerializeField] public bool RunStart;
        [SerializeField] public bool RunFinish;
        [SerializeField] public bool Jump;
        [SerializeField] public bool ItemInteract;
        [SerializeField] public bool ItemThrow;
        [SerializeField] public bool ToggleInventory;
        [SerializeField] public bool Aim;
        [SerializeField] public bool Shoot;
        [SerializeField] public bool Reload;
        [SerializeField] public bool ChangeWeaponMode;
        [SerializeField] public bool ChangeActiveInventorySlot1;
        [SerializeField] public bool ChangeActiveInventorySlot2;
        [SerializeField] public bool ChangeActiveInventorySlot3;
        [SerializeField] public bool ChangeActiveInventorySlot4;
        [SerializeField] public bool ChangeActiveInventorySlot5;
        [SerializeField] public bool ChangeActiveInventorySlot6;
        [SerializeField] public bool ChangeActiveInventorySlot7;
    }

    [System.Serializable]
    internal struct MouseInput
    {
        [SerializeField] public int Index;
        [SerializeField] public float MouseX;
        [SerializeField] public float MouseY;
    }
    public class HumanoidInputActionsRecorder : MonoBehaviour
    {
        [SerializeField] private string keyboardRecordFilePath;
        [SerializeField] private string mouseRecordFilePath;

        private KeyboardInput keyboardInput;
        private readonly List<KeyboardInput> keyboardInputs = new();

        private MouseInput mouseInput;
        private readonly List<MouseInput> mouseInputs = new();

        private void SaveInputs<T>(T[] inputArr, string path)
        {
            string json = Helper.ArrayToJSON<T>(inputArr);
            File.WriteAllText($@"{path}", json);
        }

        public void SetRunStart(bool toSet) => keyboardInput.RunStart = toSet;
        public void SetRunFinish(bool toSet) => keyboardInput.RunFinish = toSet;
        public void SetJump(bool toSet) => keyboardInput.Jump = toSet;
        public void SetItemInteract(bool toSet) => keyboardInput.ItemInteract = toSet;
        public void SetItemThrow(bool toSet) => keyboardInput.ItemThrow = toSet;
        public void SetToggleInventory(bool toSet) => keyboardInput.ToggleInventory = toSet;
        public void SetAim(bool toSet) => keyboardInput.Aim = toSet;
        public void SetShoot(bool toSet) => keyboardInput.Shoot = toSet;
        public void SetReload(bool toSet) => keyboardInput.Reload = toSet;
        public void SetChangeWeaponMode(bool toSet) => keyboardInput.ChangeWeaponMode = toSet;
        public void SetChangeActiveInventorySlot1(bool toSet) => keyboardInput.ChangeActiveInventorySlot1 = toSet;
        public void SetChangeActiveInventorySlot2(bool toSet) => keyboardInput.ChangeActiveInventorySlot2 = toSet;
        public void SetChangeActiveInventorySlot3(bool toSet) => keyboardInput.ChangeActiveInventorySlot3 = toSet;
        public void SetChangeActiveInventorySlot4(bool toSet) => keyboardInput.ChangeActiveInventorySlot4 = toSet;
        public void SetChangeActiveInventorySlot5(bool toSet) => keyboardInput.ChangeActiveInventorySlot5 = toSet;
        public void SetChangeActiveInventorySlot6(bool toSet) => keyboardInput.ChangeActiveInventorySlot6 = toSet;
        public void SetChangeActiveInventorySlot7(bool toSet) => keyboardInput.ChangeActiveInventorySlot7 = toSet;

        private void OnEnable()
        {
            PlayerInputManager.HumanoidInputActions.EventRunStart += () => SetRunStart(true);
            PlayerInputManager.HumanoidInputActions.EventRunFinish += () => SetRunFinish(true);
            PlayerInputManager.HumanoidInputActions.EventJump += () => SetJump(true);
            PlayerInputManager.HumanoidInputActions.EventItemInteract += () => SetItemInteract(true);
            PlayerInputManager.HumanoidInputActions.EventItemThrow += () => SetItemThrow(true);
            PlayerInputManager.HumanoidInputActions.EventToggleInventory += () => SetToggleInventory(true);
            PlayerInputManager.HumanoidInputActions.EventAim += () => SetAim(true);
            PlayerInputManager.HumanoidInputActions.EventShoot += () => SetShoot(true);
            PlayerInputManager.HumanoidInputActions.EventReload += () => SetReload(true);
            PlayerInputManager.HumanoidInputActions.EventChangeWeaponMode += () => SetChangeWeaponMode(true);
            PlayerInputManager.HumanoidInputActions.EventChangeActiveInventorySlot1 += () => SetChangeActiveInventorySlot1(true);
            PlayerInputManager.HumanoidInputActions.EventChangeActiveInventorySlot2 += () => SetChangeActiveInventorySlot2(true);
            PlayerInputManager.HumanoidInputActions.EventChangeActiveInventorySlot3 += () => SetChangeActiveInventorySlot3(true);
            PlayerInputManager.HumanoidInputActions.EventChangeActiveInventorySlot4 += () => SetChangeActiveInventorySlot4(true);
            PlayerInputManager.HumanoidInputActions.EventChangeActiveInventorySlot5 += () => SetChangeActiveInventorySlot5(true);
            PlayerInputManager.HumanoidInputActions.EventChangeActiveInventorySlot6 += () => SetChangeActiveInventorySlot6(true);
            PlayerInputManager.HumanoidInputActions.EventChangeActiveInventorySlot7 += () => SetChangeActiveInventorySlot7(true);
        }

        private void OnDisable()
        {
            SaveInputs(keyboardInputs.ToArray(), keyboardRecordFilePath);
            SaveInputs(mouseInputs.ToArray(), mouseRecordFilePath);
        }

        private void ResetKeyboardInputs()
        {
            SetRunStart(false);
            SetRunFinish(false);
            SetJump(false);
            SetItemInteract(false);
            SetItemThrow(false);
            SetToggleInventory(false);
            SetAim(false);
            SetShoot(false);
            SetReload(false);
            SetChangeWeaponMode(false);
            SetChangeActiveInventorySlot1(false);
            SetChangeActiveInventorySlot2(false);
            SetChangeActiveInventorySlot3(false);
            SetChangeActiveInventorySlot4(false);
            SetChangeActiveInventorySlot5(false);
            SetChangeActiveInventorySlot6(false);
            SetChangeActiveInventorySlot7(false);
        }

        private void RecordKeyboard()
        {
            keyboardInput.Move = PlayerInputManager.HumanoidInputActions.Move;
            keyboardInputs.Add(keyboardInput);
            ResetKeyboardInputs();
            keyboardInput.Index++;
        }

        private void RecordMouse()
        {
            mouseInput.MouseX = PlayerInputManager.HumanoidInputActions.MouseX;
            mouseInput.MouseY = PlayerInputManager.HumanoidInputActions.MouseY;
            mouseInputs.Add(mouseInput);
            mouseInput.Index++;
        }

        private void FixedUpdate()
        {
            RecordKeyboard();
        }

        private void Update()
        {
            RecordMouse();
        }
    }
}
