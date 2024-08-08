using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public float targetRotationX = -90f;  // 목표 x 회전값
    public float duration = 1f;           // 회전 애니메이션 시간

    private Vector3 startRotation;
    private Vector3 endRotation;
    private float elapsedTime = 0f;
    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 확인
        {
            StartRotation();
        }

        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            Vector3 result = Vector3.Lerp(startRotation, endRotation, t);

            print($"Start: {startRotation}, End: {endRotation}, Lerp: {result}");
            //transform.rotation = Quaternion.Euler(result);
            transform.eulerAngles = result;
            if (t >= 1f)
            {
                isRotating = false;
            }
        }
    }

    void StartRotation()
    {
        //transform.eulerAngles = transform.eulerAngles;

        // 현재 회전값을 기준으로 x축에 대한 회전값 설정
        //startRotation = new Vector3(targetRotationX, transform.eulerAngles.z, transform.eulerAngles.y);
        //targetRotationX -= 90;
        //endRotation = new Vector3(targetRotationX, transform.eulerAngles.y, transform.eulerAngles.z);

        Quaternion originRot = transform.rotation;


        elapsedTime = 0f;
        isRotating = true;
    }
}