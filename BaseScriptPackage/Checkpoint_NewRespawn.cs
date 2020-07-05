using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class Checkpoint_NewRespawn : MonoBehaviour
{
    //Attach this script to an object to turn it into a checkpoint. Make sure there is a 2D collider set to trigger on the gameobject

    private Vector3 position;
    private GameManager_SideScroller gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager_SideScroller>();
        position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.respawnPosition = position;
        }

    }
}
