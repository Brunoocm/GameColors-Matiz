using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class PrismaMovement : MonoBehaviour
    {

        public float smoothSpeed;
        public Vector3 offset;

        private void FixedUpdate()
        {

            if (CharacterStats.playerObj != null)
            {                

                Vector3 desiredPosition = CharacterStats.playerObj.transform.position + offset;
                Vector3 smothedposition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smothedposition;
            }
         
        }
    }
}