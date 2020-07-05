using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLife : MonoBehaviour
{

    //Attach this script to a projectile to give it a duration before it is destroyed

    public float lifeTime = 2.0f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }


    void Update()
    {
        if (Time.time - startTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
