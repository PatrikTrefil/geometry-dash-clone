using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] float smoothFactor;
    [SerializeField] Vector3 offset; // camera offset relative to player

    void smoothCameraMovement()
    {
        Vector3 targetCameraPosition = targetObject.position + offset;
        float distanceBetween = (targetCameraPosition - transform.position).magnitude;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetCameraPosition, smoothFactor * Mathf.Pow(distanceBetween, 5) * Time.deltaTime);
        transform.position = smoothPosition;
    }

    void FixedUpdate()
    {
        smoothCameraMovement();
    }
}
