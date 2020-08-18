using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement_SideScroller : MonoBehaviour
{
    //This script handles all of the players movement through walking and jumping. Uses a rigidbody to handle physics
    // NOTE: This script is intended to work with blend trees for animation.


    /* ============================================
     * Check out this video for other ways to implement movement if you prefer to not use this script!
     * https://www.youtube.com/watch?v=c3iEl5AwUF8
     * ============================================
     */




    [Header("Movement Behavior")]
    public float walkSpeed = 500;  
    public float maxJumps = 1;
    public float jumpForce = 500;
    private float jumps;
    [Tooltip("Leave true if you want to be able to change directions while in the air. Otherwise, movement input cannot change the player's velocity while airborne.")]
    public bool canMoveInAir = true;

    [Header("Ground Check Variables")]
    public LayerMask groundLayer;
    Collider2D p_collider;
    public float boxCastDepth = 0.1f;
    public bool developerShowBoxCastBounds = false;

    //Component variables
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;


    //Animator/Movement Variables
    [HideInInspector]
    public float horizontal;
    [HideInInspector]
    public float lastHorizontal = 0;
    [HideInInspector]
    public float vertical;
    private Animator animator;

    //State Booleans
    [HideInInspector]
    public bool frozen = false;
    [HideInInspector]
    public bool isGrounded = true;
    private Vector3 scale;
    [HideInInspector]
    public bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        jumps = maxJumps;
        scale = transform.localScale;
        p_collider = GetComponent<Collider2D>();

        //Generally good to have 
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the float values from the movement keys WASD and arrow keys. These are used to calculate movement direction and what animations to play
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (jumped && rb.velocity.y < 0)
        {
            jumped = false;
        }

        if (!jumped)
        {
            ComputeIsGrounded(); //Finds out if the player is grounded or not
        }
        
        
        computeLastHorizontal();

        if (isGrounded || canMoveInAir)
        {
            if (!frozen)
            {
                //Sets the corresponding values to the animator so that the correct animation is played in the blend tree
                animator.SetFloat("Horizontal", horizontal);
                animator.SetFloat("Vertical", vertical);

                

                //Creates a new movement vector that will determine direction our character is going and adding in the current upwards and/or downwards velocity of our object
                Vector2 velocity = (transform.right * horizontal) * walkSpeed * Time.fixedDeltaTime;
                velocity.y = rb.velocity.y;
                rb.velocity = velocity;

                if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
                {
                    //We set the y value to 0 to negate gravity, then add the upwards force for our jump
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(transform.up * jumpForce);
                    --jumps;
                    jumped = true;
                    animator.SetBool("Jumped", jumped);
                }
            }
            else
            {
                //If the character is frozen, we stop moving immediately. 
                rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
                animator.SetFloat("Horizontal", 0);
            }
        }
        

        

    }

    /// <summary>
    /// This method is used to determine the last way we were moving so that our idle can face either left or right.
    /// </summary>
    public void computeLastHorizontal()
    {
        if (horizontal != 0 && horizontal != lastHorizontal)
        {
            lastHorizontal = horizontal;
        }
        if (isGrounded || canMoveInAir)
        {
            if (lastHorizontal >= 0)
            {
                //spriteRenderer.flipX = true;
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
            else
            {
                //spriteRenderer.flipX = false;
                transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
            }
        }
        
    }

    void ComputeIsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(p_collider.bounds.center, p_collider.bounds.size, 0.0f, Vector2.down, boxCastDepth, groundLayer);

        //Debug stuff
        BoxCastDebugRays(hit);

        if (hit.collider != null)
        {
            if (jumped)
            {
                Debug.Log($"We are grounded {Time.time}");
            }
            
            jumps = maxJumps;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            //jumped = false;
        } 
        else
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("Jumped", jumped);
            isGrounded = false;
            
        }

    }

    void BoxCastDebugRays(RaycastHit2D hit)
    {
        if (developerShowBoxCastBounds)
        {
            Color color;
            if (hit.collider != null)
            {
                color = Color.green;
            }
            else
            {
                color = Color.red;
            }
            Debug.DrawRay(p_collider.bounds.center + new Vector3(p_collider.bounds.extents.x, 0), Vector2.down * (p_collider.bounds.extents.y + boxCastDepth), color);
            Debug.DrawRay(p_collider.bounds.center - new Vector3(p_collider.bounds.extents.x, 0), Vector2.down * (p_collider.bounds.extents.y + boxCastDepth), color);
            Debug.DrawRay(p_collider.bounds.center + new Vector3(-p_collider.bounds.extents.x, -p_collider.bounds.extents.y - boxCastDepth), Vector2.right * (p_collider.bounds.extents.x * 2), color);
        }
    }

}
