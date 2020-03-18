using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().takeDamage(damage);
        }
    }

}
