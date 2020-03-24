using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject player;

    public Transform portal;
    public Grid grid;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("World").GetComponent<Grid>();
        player = GameObject.FindGameObjectWithTag("Player");
        destination = grid.CellToWorld(grid.WorldToCell(portal.position)) - grid.cellSize/2;
    }

    public void moveCharacter()
    {
        Debug.Log("Enter");
        // playerMovement.moving = false;
        // playerMovement.move = Vector3.zero;
        player.transform.position = destination;
    }
}
