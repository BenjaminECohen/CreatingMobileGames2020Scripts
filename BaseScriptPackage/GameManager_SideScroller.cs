using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_SideScroller : MonoBehaviour
{
    /* Attach this script to an empty gameObject
     * 
     * This Game Manager is needed for most scripts in the starter package and for the Inventory System DLC
     * 
     */

    //Player Variables
    [Header("Player Variables")]
    [Tooltip("Your Player/Character")]
    public GameObject player;
    private PlayerMovement_SideScroller playerMov;
    private Health playerHealth;

    [Header("Respawn")]
    public Vector3 respawnPosition;
    [Tooltip("The respawn position on start is set to the first spot the player is in on start")]
    public bool setRespawnToPlayerPlacement = false;

    [HideInInspector]
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        playerMov = player.GetComponent<PlayerMovement_SideScroller>();
        playerHealth = player.GetComponent<Health>();
        if (setRespawnToPlayerPlacement)
        {
            respawnPosition = player.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Closes the game executable
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Respawns the player at the respawn position variable in the GameManager
    /// </summary>
    public void respawn()
    {
        player.transform.position = respawnPosition;
        playerHealth.health = playerHealth.maxHealth;
    }

    /// <summary>
    /// Sets a new position for player respawn
    /// </summary>
    /// <param name="newPosition">The new respawn position</param>
    public void setRespawn(Vector3 newPosition)
    {
        respawnPosition = newPosition;
    }
}
