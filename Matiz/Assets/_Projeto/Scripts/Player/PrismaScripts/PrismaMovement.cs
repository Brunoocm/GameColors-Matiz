using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class PrismaMovement : MonoBehaviour
    {

        public float smoothSpeed;
        public Vector3 offset;
        SpriteRenderer sprite => GetComponentInChildren<SpriteRenderer>();

        ProgressionManager progressionManager => FindObjectOfType<ProgressionManager>();

        private void Start()
        {
            if (progressionManager.cinza)
            {
                sprite.color = Color.white;
            } 
            if (progressionManager.vermelho)
            {
                sprite.color = Color.red;

            }
            if (progressionManager.azul)
            {
                sprite.color = Color.blue;

            }
            if (progressionManager.verde)
            {
                sprite.color = Color.green;

            }
        }
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