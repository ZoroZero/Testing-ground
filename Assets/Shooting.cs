using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform fire_Point;

    public GameObject arrow_Prefab;

    //Arrow force
    public float arrow_Force = 20f;

    //Player direction 
    public PlayerMovement playerMovement;

    // Animator
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Change animation
        animator.SetTrigger("is_Shooting");

        // Create arrow
        Vector2 force = Vector2.zero;
        if(playerMovement.direction == PlayerMovement.Direction.left)
        {
            force.x = -1;
            force.y = 0;
            fire_Point.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (playerMovement.direction == PlayerMovement.Direction.right)
        {
            force.x = 1;
            force.y = 0;
            fire_Point.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (playerMovement.direction == PlayerMovement.Direction.up)
        {
            force.x = 0;
            force.y = 1;
            fire_Point.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (playerMovement.direction == PlayerMovement.Direction.down)
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
