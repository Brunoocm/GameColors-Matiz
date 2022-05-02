using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class PrismaMovement : MonoBehaviour
    {
        public Transform target;

        public float smoothSpeed;
        public Vector3 offset;

        private void FixedUpdate()
        {

            if (target == null)
            {
                target = FindObjectOfType<CharacterStats>().gameObject.transform;
            }
            Vector3 desiredPosition = target.position + offset;
            Vector3 smothedposition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smothedposition;
        }
    }
}