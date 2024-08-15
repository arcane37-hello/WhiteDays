using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // 기본 체력
    public int currentHealth;
    public bool hasMap = false;
    public bool ishiding = false;

    public Canvas gameCanvas; // 캔버스 UI를 public 변수로 선언

    void Start()
    {
        currentHealth = maxHealth;

        // 초기 캔버스 상태 설정
        UpdateCanvasVisibility();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print("체력저하");
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            UpdateCanvasVisibility(); // 체력 감소 후 캔버스 상태 업데이트
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        print("체력회복");
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateCanvasVisibility(); // 체력 회복 후 캔버스 상태 업데이트
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

    public void GetMap()
    {
        hasMap = true;
    }

    // 캔버스 활성화 여부를 업데이트하는 메서드
    private void UpdateCanvasVisibility()
    {
        if (gameCanvas != null)
        {
            // currentHealth가 3 미만일 때 캔버스 활성화
            gameCanvas.gameObject.SetActive(currentHealth < 3);
        }
    }
}