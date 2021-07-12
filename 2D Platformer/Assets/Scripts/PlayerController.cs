using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D theRB;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer theSR;

    public float knockbackLength, knockbackForce;
    private float knockbackCounter;

    public float bounceForce;

    public bool stopInput;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If game is paused
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            // If player didn't take damage
            if (knockbackCounter <= 0)
            {
                // Move horizontaly
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                // Check if player is on the ground
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                // Jump
                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        canDoubleJump = true;
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    }
                    else if (canDoubleJump)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        canDoubleJump = false;
                    }

                    AudioManager.instance.PlaySFX(10);
                }

                // Flip asset horizontaly if player go left
                if (theRB.velocity.x < 0)
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockbackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    // facing to right
                    theRB.velocity = new Vector2(-knockbackForce, theRB.velocity.y);
                }
                else
                {
                    // facing to left
                    theRB.velocity = new Vector2(knockbackForce, theRB.velocity.y);
                }

                anim.SetTrigger("hurt");
            }
        }

        // Animations
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        // Stop player and knockback him
        theRB.velocity = new Vector2(0f, knockbackForce);
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }
}
