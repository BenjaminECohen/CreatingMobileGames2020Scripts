using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Extra : MonoBehaviour
{
    /* This script is used for different types of pickups. Also contains extra functionality to work with the Inventory System DLC.
     * There are 3 types of pickups: Health, Score, and Item.
     * 1. Health restores health to the player through their Health script
     * 2. Score adds points to the player through the GameManager
     * 3. Item adds the pickup to the player's inventory to be used as a key
     */


    public enum PickupType
    {
        Health,
        Score,
        [Tooltip("Item can only be used if you have the Inventory System DLC")]
        Item

    }
    public PickupType type;

    public int healAmount;
    public int scoreAmount;

    private GameManager_SideScroller gameManager;

    public bool isWeapon = false;

    [Header("Inventory System: Item Details")]
    public string itemName = "Item";
    public int itemID = 0;
    public bool destroyOnUse = false;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_SideScroller>();
        if (type == PickupType.Item)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == PickupType.Health && collision.tag == "Player")
        {
            collision.TryGetComponent<Health>(out Health health);
            if (health && health.health != health.maxHealth)
            {
                health.addHealth(healAmount);
                this.gameObject.SetActive(false);
            }
        }
        if (type == PickupType.Score && collision.tag == "Player")
        {
            gameManager.score += scoreAmount;
            this.gameObject.SetActive(false);
        }
        if (type == PickupType.Item && collision.tag == "Player")
        {
            collision.TryGetComponent<Player_Inventory>(out Player_Inventory inv);
            inv.AddItemToInventory(new Player_Inventory.Item(itemName, itemID, spriteRenderer.sprite, spriteRenderer.color, destroyOnUse));

            if (isWeapon)
            {
                transform.parent = gameManager.player.transform;
                transform.localPosition = Vector3.zero; // Zero's position

                gameManager.player.GetComponent<PlayerAttack_SideScroller>().weapon = GetComponent<Weapon>(); //Weapon Equipped
            }
            else
            {
                this.gameObject.SetActive(false);

            }

            

        }
    }

}
