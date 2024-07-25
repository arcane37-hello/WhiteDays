using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 배열 (Array)
    Transform[] camPositions = new Transform[1];

    // 리스트 (List)
    List<Transform> camList = new List<Transform>();

    FollowCamera followCam;
    float currentRate = 0;

    void Start()
    {
        // FollowCamera 컴포넌트를 캐싱한다.
        followCam = Camera.main.gameObject.GetComponent<FollowCamera>();

        camList.Add(transform.GetChild(0));

        ChangeCamTarget(0);

    }

    void Update()
    {
        currentRate = Mathf.Clamp(currentRate, 0.0f, 0.0f);

        Camera.main.transform.position = Vector3.Lerp(camList[0].position, camList[0].position, currentRate);

    }

    void ChangeCamTarget(int targetNum)
    {
        // 메인 카메라의 FollowCamera 클래스에 있는 target에 0번 요소를 넣는다.

        if (followCam != null)
        {
            // followCam.target = camPositions[targetNum];
            followCam.target = camList[targetNum];
        }
    }
}
