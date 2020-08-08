using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class PlayerAttack_SideScroller_Durability : MonoBehaviour
{

    //This script handles the animation of attacking on a button press. 
    
    //It is also necessary to use for the weapon switching script and attack behavior script

    private Animator animator;
    public Weapon weapon;

    private int durability;
    public int MAX_DURABILITY = 1;

    // Start is called before the first frame update
    void Start()
    {
        durability = MAX_DURABILITY;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !animator.GetBool("Attack"))
        {
            if (weapon && durability > 0)
            {
                //ATTACK!
                durability--;
                animator.SetBool("Attack", true);


                if (durability <= 0)
                {
                    weapon = null;
                    durability = MAX_DURABILITY;
                }

            }
            
        }
    }
}
