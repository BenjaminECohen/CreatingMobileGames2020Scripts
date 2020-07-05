using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehavior : StateMachineBehaviour
{
    private Weapon weapon;
    private int directionalForceMultiplier;
    [Tooltip("Set this to true if you don't turn the weapon collider on and off in the animation")]
    public bool handleWeaponCollider = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //If there is an AudioSource component on the gameobject the animtor is a component of, we can use that to play an audioclip
        AudioSource audio = animator.GetComponent<PlayerAudioHandler>().attackAudioSource;
        if (audio)
        {
            AudioClip clip = animator.GetComponent<PlayerAttack_SideScroller>().weapon.attackSoundEffect;
            if (clip)
            {
                audio.PlayOneShot(clip);
            }
            
        }
        weapon = animator.GetComponent<PlayerAttack_SideScroller>().weapon;
        
        if (handleWeaponCollider)
        {
            weapon.gameObject.GetComponent<Collider2D>().enabled = true;
        }
        

        if (animator.GetComponent<PlayerMovement_SideScroller>().lastHorizontal > 0)
        {
            directionalForceMultiplier = 1;
        }
        else
        {
            directionalForceMultiplier = -1;
        }

        if (weapon.weaponType == Weapon.WeaponType.Ranged)
        {
            if (weapon.instantFire)
            {
                FireProjectile(animator);
            }
            else
            {
                //Implement later
            }
        }




    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("State Exiting");
        if (handleWeaponCollider)
        {
            weapon.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void FireProjectile(Animator animator)
    {
        GameObject projectile = Instantiate(weapon.projectilePrefab, weapon.projectileSpawnPosition.position, Quaternion.identity);
        Weapon.Projectile projectileStats = projectile.GetComponent<Weapon>().projectile;
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        projectileRB.gravityScale = projectileStats.projectileGravityScale;
        projectileRB.AddForce(projectile.transform.right * directionalForceMultiplier * projectileStats.projectileSpeed);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
