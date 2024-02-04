using UnityEngine;

namespace U3.Player
{
    [System.Serializable]
    public class InventorySlotSetting
    {
        public bool IsAvailable;
        public bool IsSelectable;
        public int SlotIndex;
        public string SlotUIParentCode;
        public Item.ItemType[] AcceptableItemTypes;
        public GameObject SlotUIPrefab;
        public GameObject AssignedItem;
    }
    [System.Serializable]
    public class InventorySettings
    {
        public int LabelFontSize;
        public int ItemSearchBufferSize;
        public int BackpackCapacity;
        public int SelectedSlotIndex;
        public float ItemCheckRate;
        public float ItemSearchRange;
        public float ItemSearchRadius;
        public string InventoryFullMessageText;
        public Vector2 LabelDimensions;
        public Color LabelColor;
        public Color SlotDefaultColor;
        public Color SlotSelectedColor;
        public InventorySlotSetting[] InventorySlots;
        public GameObject[] BackpackItems;
    }
    [System.Serializable]
    public class SoundSettings
    {
        public AudioClip JumpSound;
        public AudioClip LandSound;
        public AudioClip[] StepSounds;
    }
    [System.Serializable]
    public struct ControllerSettings
    {
        public float WalkSpeed;
        public float RunSpeed;
        public float JumpSpeed;
        public float GravityMultiplayer;
        public float InertiaCoefficient;
        public float LookClamp;
        public Vector2 LookSensitivity;
        public Vector2 StepSpeed;
        public Vector2 HeadBobSpeed;
        public Vector2 HeadBobMagnitude;
        public Vector2 HeadBobMultiplayer;
    }
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        public ControllerSettings Controller;
        public SoundSettings Sound;
        public InventorySettings Inventory;
    }
}
