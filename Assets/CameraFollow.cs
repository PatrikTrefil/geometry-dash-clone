using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;
    public float smoothFactor;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 targetCameraPosition = targetObject.position + offset;
        float distanceBetween = (targetCameraPosition - transform.position).magnitude;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetCameraPosition, smoothFactor * Mathf.Pow(distanceBetween, 5) * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
