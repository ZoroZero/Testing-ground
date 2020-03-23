using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int arrow_Damage = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().takeDamage(arrow_Damage);
           
        }
        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
