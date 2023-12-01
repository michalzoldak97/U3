using UnityEngine;

namespace U3.Player
{
    [System.Serializable]
    public class InventorySlotSetting
    {
        public Item.ItemType[] AcceptableItemTypes;
        public GameObject SlotUIPrefab;
    }
    [System.Serializable]
    public class InventoryContainerSetting
    {
        public string ContainerName;
        public int SlotsNum;
        public GameObject ContainerUIPrefab;
    }
    [System.Serializable]
    public class InventorySettings
    {
        public int LabelFontSize;
        public int ItemSearchBufferSize;
        public float ItemCheckRate;
        public float ItemSearchRange;
        public float ItemSearchRadius;
        public Vector2 LabelDimensions;
        public Color LabelColor;
        public InventoryContainerSetting[] InventorySlots;
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
