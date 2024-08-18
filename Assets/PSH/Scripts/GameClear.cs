using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public Image fadeImage; // 검은색 이미지 (캔버스에 포함된 이미지)
    public float fadeDuration = 1f; // 페이드 인에 걸리는 시간
    public PlayerHealth ph;

    private void Start()
    {
        // 초기에는 이미지가 보이지 않도록 설정
        if (fadeImage != null)
        {
            Color tempColor = fadeImage.color;
            tempColor.a = 0f; // 알파 값을 0으로 설정 (완전히 투명)
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
                    // 클릭한 오브젝트가 이 스크립트가 붙어 있는 오브젝트인지 확인
                    if (hit.transform == transform)
                    {
                        // 캔버스 이미지 페이드 인 시작
                        StartCoroutine(FadeInAndLoadScene());
                    }
                }
            }
        }
        // 왼쪽 마우스 버튼 클릭 감지
        
    }

    private IEnumerator FadeInAndLoadScene()
    {
        // 이미지 페이드 인 효과
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

        // 페이드 인 완료 후 알파 값을 1로 설정
        tempColor.a = 1f;
        fadeImage.color = tempColor;

        // 1초 대기 후 씬 전환
        yield return new WaitForSeconds(1f);

        // 씬 전환
        SceneManager.LoadScene(3);
    }
}