using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3;

    public void takeDamage(int damage)
    {
        Debug.Log("You take " + damage.ToString());
    }
}
