using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { IDLE, RUNNING, JUMPING, FALLING }
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        
        if (Input.GetButtonDown("Jump")) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAmimationState();
    }

    private void UpdateAmimationState()
    {
        MovementState state;

        if(dirX > 0f) 
        {
            state = MovementState.RUNNING;
            sprite.flipX = false;

        } else if (dirX < 0f)
        {
            state = MovementState.RUNNING;
            sprite.flipX = true;
        } else 
        {
            state = MovementState.IDLE;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.JUMPING;
        } else if (rb.velocity.y < -.1f)
        {
            state = MovementState.FALLING;
        }

        anim.SetInteger("state", (int) state);
    }
}
