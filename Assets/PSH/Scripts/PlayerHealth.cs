using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // 기본 체력
    public int currentHealth;
    public bool hasMap = false;
    public bool ishiding = false;
    public bool isComplete = false;
    public bool canDriver = false;
    public bool inVent = false;
    public SoloText sT;
    public AudioClip gameOver;

    public Image game1;
    public Image game2;

    void Start()
    {
        game1.enabled = false;
        game2.enabled = false;

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
        Time.timeScale = 0;
        GameManager.gm.showGameOverUI();
        AudioSource.PlayClipAtPoint(gameOver, gameObject.transform.position);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void GetMap()
    {
        hasMap = true;
        sT.Hon3();
    }

    //이미지 활성화 여부를 업데이트하는 메서드
    private void UpdateCanvasVisibility()
    {      

        if ( currentHealth < 3)
        {
            game1.enabled = true;
        }
        if ( currentHealth < 2)
        {
            game2.enabled = true;
        }
    }
}