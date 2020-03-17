using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Move speed
    public float move_Speed = 5f;

    // Rigid body
    public Rigidbody2D rb;

    // Speed vector
    public Vector2 move;


    // Animator ref
    public Animator animator;

    private enum Direction
    {
        left = 1,
        down = 2,
        up = 3,
        right = 4
    };

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", move.x);
        animator.SetFloat("vertical", move.y);
        animator.SetFloat("speed", move.sqrMagnitude);

        if (move.x < 0)
        {
            animator.SetFloat("direction", (float)Direction.left);
        }
        else if(move.x > 0)
        {
            animator.SetFloat("direction", (float)Direction.right);
        }
        else if (move.y > 0)
        {
            animator.SetFloat("direction", (float)Direction.up);
        }
        else if (move.y < 0)
        {
            animator.SetFloat("direction", (float)Direction.down);
        }
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * move_Speed * Time.fixedDeltaTime);
    }
}
