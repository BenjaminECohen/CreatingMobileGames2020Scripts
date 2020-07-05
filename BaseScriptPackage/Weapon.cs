using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class Weapon : MonoBehaviour
{

    /*This script is what deals damage to another entity that has the Health Script attached to it. 
     * Additionally, this script also affects the nature of the weapon, such as damage, who the owner is, and properties of a projectile
     */

    [System.Serializable]
    public class Projectile
    {
        [Tooltip("Set to 0 for projectile to be unnaffected by gravity.")]
        public float projectileGravityScale = 1;
        public float projectileSpeed = 100;
        
    }
    public enum Owner
    {
        Player,
        Enemy
    }
    public enum WeaponType
    {
        Melee,
        Ranged,
        Projectile
    }
    public Owner weaponOwner;
    public WeaponType weaponType;
    public int weaponDamage = 10;
    [Tooltip("Only matters for Ranged typed weapons")]
    public bool instantFire = true;
    [Tooltip("Only needs to be filled when Weapon type is Ranged. Make sure that the projectile has the Weapon Script. Make sure that the projectile has a RigidBody2D on.")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPosition;
    [Tooltip("Only matters for Projectile typed weapons. Fields to manipulate how the projectile works.")]
    public Projectile projectile;

    public AudioClip attackSoundEffect;
    Health.EntityType ownerType;

    private void Start()
    {
        if (weaponOwner == Owner.Player)
        {
            ownerType = Health.EntityType.Player;
        }
        else
        {
            ownerType = Health.EntityType.Enemy;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Health>(out Health component); 
        if (component != null) //Check to make sure the collided object has a health script
        {
            //Debug.Log(component.entityType + " vs " + this.ownerType);
            if (component.entityType != this.ownerType) //If the object is a foe, deal damage to them
            {
                //Debug.Log("Hurt");
                component.decrementHealth(weaponDamage);

                //On contact, destroy projectile
                if (weaponType == WeaponType.Projectile)
                {
                    Destroy(this.gameObject);

                }
            }
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent<Health>(out Health component);
        if (component != null) //Check to make sure the collided object has a health script
        {
            if (component.entityType != this.ownerType) //If the object is a foe, deal damage to them
            {
                component.decrementHealth(weaponDamage); 

                //On contact, destroy projectile
                if (weaponType == WeaponType.Projectile)
                {
                    Destroy(this.gameObject);

                }
            }
        }
        
    }
}
