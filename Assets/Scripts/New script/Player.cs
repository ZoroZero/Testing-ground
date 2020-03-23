using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    // Health
    public int health = 3;

    // Moving vector
    public Vector3 move;

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


    protected override void Start()
    {
        animator = GetComponent<Animator>();
        melee_Hitbox = GameObject.FindGameObjectWithTag("Player_AttackPoint").transform;
        move = Vector3.zero;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            if (move.x != 0)
                move.y = 0f;

            animator.SetFloat("horizontal", move.x);
            animator.SetFloat("vertical", move.y);
            animator.SetFloat("speed", move.sqrMagnitude);
            if (move.sqrMagnitude > 0)
            {
                attemptToMove<Portal>((int)move.x, (int)move.y);
            }
        }
        if (move.x < 0)
        {
            direction = Direction.left;
            animator.SetFloat("direction", (float)Direction.left);
        }
        else if (move.x > 0)
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


    // On cant move function
    protected override void onCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }

    public void takeDamage(int damage)
    {
        Debug.Log("You take " + damage.ToString());
    }

    // Attack function
    private void Attack()
    {
        // Attack_animation
        animator.SetTrigger("is_Attack");

        // See what enemy got hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(melee_Hitbox.position, melee_Range, enemy_Layers);

        // Damage each enemy got hit
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(melee_Damage);
        }

    }

    // Draw hitbox of attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(melee_Hitbox.position, melee_Range);
    }

    // Check if player collide with door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            Invoke("LoadNextLevel" , 1f);
        }
    }

    // Private void change to next scene
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
