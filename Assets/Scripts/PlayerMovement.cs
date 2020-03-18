using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Move speed
    public float move_Speed = 3f;

    // Rigid body
    public Rigidbody2D rb;

    // Speed vector
    public Vector2 move;


    // Animator ref
    public Animator animator;

    //Melee hit box
    public Transform melee_Hitbox;

    public float melee_Range = 0.3f;

    public int melee_Damage = 40;

    // Attack cooldown
    public float attack_Rate = 2f;

    float next_Attack_Time = 0f; 

    // Enemy layer
    public LayerMask enemy_Layers;

    // Direction
    public enum Direction
    {
        left = 1,
        down = 2,
        up = 3,
        right = 4
    };

    public Direction direction;

    private void Start()
    {
        direction = Direction.down;
    }

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
            direction = Direction.left;
            animator.SetFloat("direction", (float)Direction.left);
        }
        else if(move.x > 0)
        {
            direction = Direction.right;
            animator.SetFloat("direction", (float)Direction.right);
        }
        else if (move.y > 0)
        {
            direction = Direction.up;
            animator.SetFloat("direction", (float)Direction.up);
        }
        else if (move.y < 0)
        {
            direction = Direction.down;
            animator.SetFloat("direction", (float)Direction.down);
        }

        if (Time.time > next_Attack_Time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                next_Attack_Time = Time.time + 1f / attack_Rate;
            }
        }
    }


    private void FixedUpdate()
    {
        if (move.sqrMagnitude > 0)
        {
            Vector2 position = Vector2.zero;
            if (direction == Direction.left)
            {
                position.x = -0.35f;
                position.y = -0.45f;
            }
            else if (direction == Direction.right)
            {
                position.x = 0.35f;
                position.y = -0.45f;
            }
            else if (direction == Direction.up)
            {
                position.x = 0f;
                position.y = 0.2f;
            }
            else if (direction == Direction.down)
            {
                position.x = 0f;
                position.y = -0.9f;
            }
            melee_Hitbox.localPosition = position;
        }
        
        rb.MovePosition(rb.position + move * move_Speed * Time.fixedDeltaTime);
    }


    private void Attack()
    {
        // Attack_animation
        animator.SetTrigger("is_Attack");

        // See what enemy got hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(melee_Hitbox.position, melee_Range, enemy_Layers);

        // Damage each enemy got hit
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(melee_Damage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(melee_Hitbox.position, melee_Range);
    }
}
