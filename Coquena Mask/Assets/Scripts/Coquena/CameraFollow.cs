using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The target to follow
    public Vector3 offSet;

    [Header("Distance Settings")]
    public float desiredDistance; // Desired distance from the target
    private float currentDistance; // Current distance from the target

    [Header("Movement Settings")]
    public float speed = 1.2f; // Speed of camera follow
    public bool useLerp = true; // Toggle to use Lerp

    private Vector3 desiredPosition; // Desired position of the camera

    void Start()
    {
       
        currentDistance = Vector3.Distance(transform.position, target.position);
        desiredPosition = transform.position;
    }

    void Update()
    {
        if (target == null) return;

        if (useLerp)
        {
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * speed);
            desiredPosition = target.position - (transform.rotation * Vector3.forward * currentDistance) + offSet;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);
            Camera.main.orthographicSize = desiredDistance;
        }
        else
        {
            currentDistance = desiredDistance;
            desiredPosition = target.position - (transform.rotation * Vector3.forward * currentDistance) + offSet;
            transform.position = desiredPosition;
            Camera.main.orthographicSize = desiredDistance;
        }
    }

    public void SetDesiredDistance(float targetDistance)
    {
        desiredDistance = targetDistance;
    }
}
