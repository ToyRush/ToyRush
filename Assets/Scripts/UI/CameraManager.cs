using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{
    FollowPlayer,
    GotoEvent
}

public class CameraManager : MonoBehaviour
{
    private float shakeTime;
    private float shakeIntensity;
    [SerializeField]
    private float followIntensity;

    [SerializeField]
    CameraState cameraState;
    public Transform target;
    
    void FixedUpdate()
    {
        switch (cameraState)
        {
            case CameraState.FollowPlayer:
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, this.transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, followIntensity);
                break;
            case CameraState.GotoEvent:
                StartCoroutine("ReturnState");
                break;
        }
        if (Input.GetKeyDown(KeyCode.K))
            OnShakeCamera();
    }

    void OnShakeCamera(float shakeTime = 0.2f, float shakeIntensity=0.2f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;
        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");
    }
    IEnumerator ReturnState()
    {
        yield return new WaitForSeconds(3f);
        cameraState = CameraState.FollowPlayer;
    }
    IEnumerator ShakeByPosition()
    {
        Vector3 startPosition = transform.position;
        while (shakeTime > 0.0f)
        {
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;
    }
}
