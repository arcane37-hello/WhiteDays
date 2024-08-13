using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Fade : MonoBehaviour
{
    Renderer MyRenderer;

    IEnumerator FadeIn()
    {
        float f = 0;
        while (f <= 1)
        {
            f += 0.1f;
            Color ColorAlhpa = MyRenderer.material.color;
            ColorAlhpa.a = f;
            MyRenderer.material.color = ColorAlhpa;
            yield return new WaitForSeconds(0.02f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MyRenderer = gameObject.GetComponent<Renderer>();
        MyRenderer.material.color = new Color(1, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && MyRenderer.material.color.a <= 1)
            StartCoroutine(FadeIn());
    }
}