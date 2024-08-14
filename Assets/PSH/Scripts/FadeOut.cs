using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 2.0f; // 오브젝트가 완전히 사라지기까지 걸리는 시간
    private Material material;
    private Color initialColor;
    private bool isFadingOut = false;
    private float fadeTime = 0f;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isFadingOut = true;
            fadeTime = 0f; // Fade 시작 시간 초기화
        }

        if (isFadingOut)
        {
            fadeTime += Time.deltaTime;
            float normalizedTime = fadeTime / fadeDuration;
            float alpha = Mathf.Clamp01(1 - normalizedTime); // 알파 값을 1에서 0으로 감소시킴
            SetMaterialAlpha(alpha);

            // Fade가 끝났다면 종료
            if (alpha <= 0f)
            {
                isFadingOut = false;
            }
        }
    }

    private void SetMaterialAlpha(float alpha)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;

        // Rendering Mode가 Transparent인지 확인
        if (material.shader.name == "Universal Render Pipeline/Lit")
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
    }
}