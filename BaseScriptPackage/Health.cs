using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class Health : MonoBehaviour
{
    //This script stores the health value for an entity and has methods to decrement and increment health

    public int maxHealth = 100;
    [HideInInspector]
    public int health;
    [Tooltip("Set as true if you want the player to be able to have more health than max health")]
    public bool allowOverHealth = false;

    private GameManager_SideScroller gameManager;

    public enum EntityType
    {
        Player,
        Enemy,
        Destructable
    }
    public EntityType entityType;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameManager = FindObjectOfType<GameManager_SideScroller>();
    }

    public void decrementHealth(int value)
    {
        health -= value;
        if (health < 0)
        {
            health = 0;
        }
        //If the object is not the player, it is destroyed when health reaches 0
        if (entityType != EntityType.Player && health <= 0)
        {
            Destroy(this.gameObject);
        }
        else if (health <= 0)
        {
            gameManager.respawn();
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void addHealth(int value)
    {
        health += value;
        if (!allowOverHealth && health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
