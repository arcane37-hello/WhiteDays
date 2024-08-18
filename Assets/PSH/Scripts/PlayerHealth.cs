using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // �⺻ ü��
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

        // �ʱ� ĵ���� ���� ����
        UpdateCanvasVisibility();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print("ü������");
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            UpdateCanvasVisibility(); // ü�� ���� �� ĵ���� ���� ������Ʈ
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        print("ü��ȸ��");
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateCanvasVisibility(); // ü�� ȸ�� �� ĵ���� ���� ������Ʈ
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

    //�̹��� Ȱ��ȭ ���θ� ������Ʈ�ϴ� �޼���
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