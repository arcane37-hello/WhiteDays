using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolve : MonoBehaviour
{
    public GameObject effect;
    // 회전값을 체크할 오브젝트들
    public Transform[] targetObjects;

    // 체크를 한 후 회전할 오브젝트
    public Transform objectToRotate;

    // 회전 애니메이션에 필요한 변수들
    public float rotationDuration = 1.0f; // 회전이 완료되는 데 걸리는 시간
    private float rotationStartTime; // 회전이 시작된 시간
    private Vector3 initialRotation; // 초기 회전값
    private Vector3 targetRotation; // 목표 회전값
    private bool isRotating = false; // 회전 중인지 여부

    private void Update()
    {
        // targetObjects 배열의 모든 오브젝트의 x 회전값이 0인지 체크
        bool allXRotationZero = true;
        foreach (var targetObject in targetObjects)
        {
            if (Mathf.Abs(targetObject.eulerAngles.x % 360) > 0.01f)  // x 회전값이 0인지 확인
            {
                allXRotationZero = false;
                break;
            }
        }

        // 모든 오브젝트의 x 회전값이 0이면 회전 애니메이션을 시작
        if (allXRotationZero && !isRotating)
        {
            StartRotation();
            Destroy(effect);
        }

        // 회전 애니메이션이 진행 중이라면 회전값 업데이트
        if (isRotating)
        {
            UpdateRotation();
        }
    }

    private void StartRotation()
    {
        // 회전 애니메이션 시작 초기화
        initialRotation = objectToRotate.eulerAngles;
        targetRotation = new Vector3(initialRotation.x, 0, initialRotation.z);
        rotationStartTime = Time.time;
        isRotating = true;
    }

    private void UpdateRotation()
    {
        // 회전 애니메이션 진행 중
        float elapsedTime = Time.time - rotationStartTime;
        float t = elapsedTime / rotationDuration;

        // LerpAngle를 사용하여 부드러운 회전값 계산
        float newYRotation = Mathf.LerpAngle(initialRotation.y, targetRotation.y, t);
        objectToRotate.eulerAngles = new Vector3(initialRotation.x, newYRotation, initialRotation.z);

        // 회전이 완료되었는지 확인
        if (t >= 1.0f)
        {
            isRotating = false; // 회전 완료
        }
    }
}