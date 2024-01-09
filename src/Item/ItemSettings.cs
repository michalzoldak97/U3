using UnityEngine;

namespace U3.Item
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemSettings", order = 1)]
    public class ItemSettings : ScriptableObject
    {
        public bool KeepObjActive;
        public bool KeepColliderActive;
        public int ThrowForce;
        public ItemType ItemType;
        public string ToItemName;
        public string ToLayerName;
        public string GUITextToDisplay;
        public Vector3 OnParentPosition;
        public Vector3 OnParentRotation;
        public Sprite ItemIcon;
        public GameObject InventoryUIPrefab;
        public GameObject InventoryUIDetailsPrefab;
        public AudioClip[] CollisionSounds;
    }
}
