using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow3_5 : MonoBehaviour
{
    public Transform cameraTransform;

    private bool isFollowing = false;

    void Update()
    {
        if (!isFollowing) return;
        if (cameraTransform == null) return;

        // 즉시 따라오기 (Lerp 없음)
        cameraTransform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            cameraTransform.position.z
        );
    }

    // 따라가기 해제
    public void ReleaseCamera()
    {
        cameraTransform.position = new Vector3(0, 0, -10f);

        isFollowing = false;
    }
    public void ResetCamera()
    {
        isFollowing = true;
    }
    void LateUpdate()
    {
       // Debug.Log(cameraTransform.position);
    }
}