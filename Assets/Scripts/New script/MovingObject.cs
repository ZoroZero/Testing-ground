using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    // Move time
    public float move_Time = 0.1f;

    public bool moving = false;

    // Direction
    public enum Direction
    {
        left = 1,
        down = 2,
        up = 3,
        right = 4
    };

    public Direction direction;

    // Blocking lyaer
    public LayerMask blockingLayer;

    // Physics
    private Rigidbody2D rb;
    private BoxCollider2D box_Collider;

    private float inverse_MoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box_Collider = GetComponent<BoxCollider2D>();
        inverse_MoveTime = 1f / move_Time;
        direction = Direction.down;
    }


    // Attampt to move function
    protected virtual void attemptToMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;

        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
        {
            onCantMove(hitComponent);
        }
    }

    // Moving function
    private bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir, yDir, 0f);
        box_Collider.enabled = false;

        hit = Physics2D.Linecast(start, end, blockingLayer);

        box_Collider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(smoothMoving(end));
            return true;
        }
        return false;
    }


    // Smooth moving
    IEnumerator smoothMoving(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        moving = true;
        while (sqrRemainingDistance > float.Epsilon && moving)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb.position, end, Time.deltaTime* inverse_MoveTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb.MovePosition(newPostion);

            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        moving = false;
    }


    // Function to call when can not move
    protected abstract void onCantMove<T>(T component)
        where T : Component;
}
