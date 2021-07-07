using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D theRB;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move horizontaly
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        // Check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
        
        // Jump
        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                canDoubleJump = true;
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            } else if(canDoubleJump)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }
}
