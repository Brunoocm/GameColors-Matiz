using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismaMovement : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smothedposition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smothedposition;
    }
}
