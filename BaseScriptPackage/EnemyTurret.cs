using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public enum Behavior
    {
        [Tooltip("Fires a projectile only when the player is in the trigger area.")]
        TriggerArea,
        [Tooltip("Always fire projectiles.")]
        ConstantFire
    }

    public Behavior behavior = Behavior.ConstantFire;

    [Tooltip("In projectiles per second")]
    public float fireRate = 2;

    [Tooltip("The projectile that has the Weapon script on it.")]
    public GameObject projectilePrefab;

    [Tooltip("The spawn position of the projectile.")]
    public Transform projectileSpawnPosition;
    private Vector3 originalPositionOfSpawn;

    private SpriteRenderer spriteRenderer;

    private int directionalMultiplier = 1;


    //projectile firing
    private float lastShotTime = 0;

    [Header("Sound")]
    public bool useSoundEffect;
    public AudioSource audioSource;
    //public AudioClip attackSoundEffect;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPositionOfSpawn = projectileSpawnPosition.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Handles direction
        if (spriteRenderer.flipX)
        {
            directionalMultiplier = -1;
            projectileSpawnPosition.localPosition = new Vector3(originalPositionOfSpawn.x * -1, originalPositionOfSpawn.y, originalPositionOfSpawn.z);
        }
        else
        {
            directionalMultiplier = 1;
            projectileSpawnPosition.localPosition = originalPositionOfSpawn;
        }

        if (behavior == Behavior.ConstantFire)
        {
            TimeCheck();
        }

    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(this.projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
        Weapon.Projectile projectileStats = projectile.GetComponent<Weapon>().projectile;
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();

        Weapon wep = projectile.GetComponent<Weapon>();

        projectileRB.gravityScale = projectileStats.projectileGravityScale;
        projectileRB.AddForce(projectile.transform.right * directionalMultiplier * projectileStats.projectileSpeed);

        if (useSoundEffect && wep.attackSoundEffect)
        {
            audioSource.PlayOneShot(wep.attackSoundEffect);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (behavior == Behavior.TriggerArea)
            {
                TimeCheck();
            }
        }
        
    }


    private void TimeCheck()
    {
        if (Time.time - lastShotTime >= (1 / fireRate))
        {
            FireProjectile();
            lastShotTime = Time.time;
        }
    }


}
