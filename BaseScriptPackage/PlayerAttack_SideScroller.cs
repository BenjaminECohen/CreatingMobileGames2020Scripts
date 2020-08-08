using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class PlayerAttack_SideScroller : MonoBehaviour
{

    //This script handles the animation of attacking on a button press. 
    
    //It is also necessary to use for the weapon switching script and attack behavior script

    private Animator animator;
    public Weapon weapon;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !animator.GetBool("Attack"))
        {
            if (weapon)
            {
                //ATTACK!
                animator.SetBool("Attack", true);

            }
            
        }
    }
}
