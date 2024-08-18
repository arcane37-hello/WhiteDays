using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public Image fadeImage; // ������ �̹��� (ĵ������ ���Ե� �̹���)
    public float fadeDuration = 1f; // ���̵� �ο� �ɸ��� �ð�
    public PlayerHealth ph;

    private void Start()
    {
        // �ʱ⿡�� �̹����� ������ �ʵ��� ����
        if (fadeImage != null)
        {
            Color tempColor = fadeImage.color;
            tempColor.a = 0f; // ���� ���� 0���� ���� (������ ����)
            fadeImage.color = tempColor;
        }
    }

    private void Update()
    {
        if(ph.isComplete == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Ŭ���� ������Ʈ�� �� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ���� Ȯ��
                    if (hit.transform == transform)
                    {
                        // ĵ���� �̹��� ���̵� �� ����
                        StartCoroutine(FadeInAndLoadScene());
                    }
                }
            }
        }
        // ���� ���콺 ��ư Ŭ�� ����
        
    }

    private IEnumerator FadeInAndLoadScene()
    {
        // �̹��� ���̵� �� ȿ��
        float elapsedTime = 0f;
        Color tempColor = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            tempColor.a = alpha;
            fadeImage.color = tempColor;
            yield return null;
        }

        // ���̵� �� �Ϸ� �� ���� ���� 1�� ����
        tempColor.a = 1f;
        fadeImage.color = tempColor;

        // 1�� ��� �� �� ��ȯ
        yield return new WaitForSeconds(1f);

        // �� ��ȯ
        SceneManager.LoadScene(3);
    }
}