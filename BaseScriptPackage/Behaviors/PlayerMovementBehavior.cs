using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : StateMachineBehaviour
{
    private PlayerAudioHandler audio;
    public AudioClip walkingSound;
    private float audioVolume = 1.0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!audio)
        {
            audio = animator.GetComponent<PlayerAudioHandler>();
            audio.walkAudioSource.clip = walkingSound;
            audioVolume = audio.walkAudioSource.volume;
            audio.walkAudioSource.volume = 0.0f;
            audio.walkAudioSource.Play();
            
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.GetFloat("Horizontal") != 0 && animator.GetBool("isGrounded"))
        {
            audio.walkAudioSource.volume = audioVolume;
        }
        else
        {
            audio.walkAudioSource.volume = 0.0f;
        }
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
