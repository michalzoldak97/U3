﻿using UnityEngine;

namespace U3.Player.Inventory
{
    public interface IInventoryUIEventsMaster
    {
        public void CallEventOnItemButtonDrop(IItemButton itemButton, IInventoryDropArea dropArea);
        public void CallEventItemFocused(Transform item);
        public void CallEventItemUnfocused(Transform item);
    }
}
