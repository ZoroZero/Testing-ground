using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Rigid body
    public Rigidbody2D rb;

    // Speed vector
    public Vector3 move;

    // Move time
    public float move_Time = 0.5f;

    private float inverse_MoveTime;

    public bool moving = false;


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

    public LayerMask blockingLayer;
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

        inverse_MoveTime = 1f / move_Time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("horizontal", move.x);
            animator.SetFloat("vertical", move.y);
            animator.SetFloat("speed", move.sqrMagnitude);
            if(move.sqrMagnitude > 0)
            {
                Move();
            }
        }
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
    }


    // Moving function
    private void Move()
    {
        Vector2 end = rb.transform.position + move;
        RaycastHit2D hit = Physics2D.Linecast(rb.transform.position, end, blockingLayer);
        if (hit.transform == null) {
            StartCoroutine(SmoothMovement(end));
        }
        
    }

    // Smooth moving
    IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (rb.transform.position - end).sqrMagnitude;
        float elapsed = 0F;
        moving = true;
        while (sqrRemainingDistance > float.Epsilon && moving)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb.position, end, elapsed/ move_Time);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb.MovePosition(newPostion);

            elapsed += Time.deltaTime;

            sqrRemainingDistance = (rb.transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        moving = false;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            moving = false;
            move = Vector3.zero;
            enabled = false;
            StartCoroutine(awake());
            collision.GetComponent<Portal>().moveCharacter();
        }
    }

    IEnumerator awake()
    {
        yield return new WaitForSeconds(0.25f);
        enabled = true;
    }
}
