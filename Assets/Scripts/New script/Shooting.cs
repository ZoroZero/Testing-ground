using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform fire_Point;

    public GameObject arrow_Prefab;

    // Get player script
    public Player player;

    //Arrow force
    public float arrow_Force = 20f;

    // Animator
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        fire_Point = player.melee_Hitbox;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot(player.direction);
        }
    }

    void Shoot(Player.Direction direction)
    {
        // Change animation
        animator.SetTrigger("is_Shooting");

        // Create arrow
        Vector2 force = Vector2.zero;
        if(direction == Player.Direction.left)
        {
            force.x = -1;
            force.y = 0;
            fire_Point.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction == Player.Direction.right)
        {
            force.x = 1;
            force.y = 0;
            fire_Point.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (direction == Player.Direction.up)
        {
            force.x = 0;
            force.y = 1;
            fire_Point.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Player.Direction.down)
        {
            force.x = 0;
            force.y = -1;
            fire_Point.rotation = Quaternion.Euler(0, 0, 180);
        }

        GameObject arrow = Instantiate(arrow_Prefab, fire_Point.position, fire_Point.rotation);
        // Apply force to arrow
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        
        rb.AddForce(force * arrow_Force, ForceMode2D.Impulse);
    }
}
