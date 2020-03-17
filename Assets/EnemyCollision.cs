using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
