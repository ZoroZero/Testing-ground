using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int max_Health = 100;

    int current_Health;

    // Animator
    public Animator animator;

    // Init
    private void Start()
    {
        current_Health = max_Health;
    }
    
    // Take damage
    public void takeDamage(int damage)
    {
        current_Health -= damage;
        if(current_Health <= 0) {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("is_Death", true);
        // Delete from play

        Destroy(gameObject, 1f);
    }
}
