using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    //Attach to the player to give them an inventory. Stores Item type pickups

    public List<Item> inventory;

    [System.Serializable]
    public class Item
    {
        public Item(string itemName, int itemID, bool destroyOnUse)
        {
            this.itemName = itemName;
            this.itemID = itemID;
            this.destroyOnUse = destroyOnUse;
        }
        public string itemName;
        public int itemID;
        public bool destroyOnUse;
    }
}
