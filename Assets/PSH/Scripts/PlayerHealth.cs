using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // �⺻ ü��
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print("ü������");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died");
        gameObject.SetActive(false);
        GameManager.gm.showGameOverUI();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}