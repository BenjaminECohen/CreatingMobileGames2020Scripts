using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Inventory : MonoBehaviour
{
    //Attach to the player to give them an inventory. Stores Item type pickups
    
    public List<Item> inventory;

    [System.Serializable]
    public class Item
    {
        public Item(string itemName, int itemID, Sprite sprite, Color color, bool destroyOnUse)
        {
            this.itemName = itemName;
            this.itemID = itemID;
            this.sprite = sprite;
            this.color = color;
            this.destroyOnUse = destroyOnUse;
        }
        public string itemName;
        public int itemID;
        public bool destroyOnUse;
        public Sprite sprite;
        public Color color;
    }

    [Header("UI")]
    public bool useUI = false; 
    public Image invisImagePrefab;
    public GameObject inventoryImageEmpty;
    List<Image> inventoryImageList;
    public float padding = 0f;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<Item>();
        inventoryImageList = new List<Image>();
    }

    public void AddItemToInventory(Item newItem)
    {
        inventory.Add(newItem);
        if (useUI)
        {
            AddItemToUI(newItem);
        }

        
    }

    public void RemoveItemFromInventory(Item item)
    {
        inventory.Remove(item);
        if (useUI)
        {
            removeItemFromUI();
        }
        
    }

    void AddItemToUI(Item newItem)
    {
        Image newImage = Instantiate(invisImagePrefab, inventoryImageEmpty.transform);
        newImage.transform.SetParent(inventoryImageEmpty.transform, false);
        Vector3 newPosition = new Vector3(newImage.GetComponent<RectTransform>().rect.width * inventoryImageList.Count + (padding * inventoryImageList.Count),
            newImage.GetComponent<RectTransform>().localPosition.y, newImage.GetComponent<RectTransform>().localPosition.z);

        newImage.GetComponent<RectTransform>().localPosition = newPosition;
        newImage.sprite = newItem.sprite;
        newImage.color = newItem.color;
        inventoryImageList.Add(newImage);
    }

    void removeItemFromUI()
    {
        inventoryImageList.Clear(); //Clear list first

        //Destroy all UI Elements
        var i = inventoryImageEmpty.GetComponentsInChildren<Image>();
        foreach (Image t in i)
        {
            Destroy(t);
        }

        //Reinitialize UI
        foreach(Item item in inventory)
        {
            AddItemToUI(item);
        }

    }

}
