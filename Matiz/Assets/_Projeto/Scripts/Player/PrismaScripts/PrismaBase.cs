using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class PrismaBase : MonoBehaviour
    {
        public float comeBackTime; //tempo para voltar o prisma, mesmo que o jogador nao pegue
        public float force;
        public GameObject prismaVisual;

        private bool canTake;

        public CharacterStats characterStats;
        Rigidbody rb => gameObject.GetComponent<Rigidbody>();
        void Start()
        {
            Inpulse();
            StartCoroutine(DropObject());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Inpulse();
            }
            if (CharacterStats.playerObj == null) Destroy(gameObject);
        }

        void Inpulse()
        {
            float angle = Random.Range(0, 360);
            Vector3 angleend = new Vector3(angle, angle, angle);

            rb.AddTorque(angleend, ForceMode.Impulse);
            rb.AddRelativeForce(Random.onUnitSphere / 2 * force);
        }

        IEnumerator DropObject()
        {
            yield return new WaitForSeconds(1f);
            canTake = true;

            yield return new WaitForSeconds(comeBackTime);
            //characterStats.GiveShield();
            //Destroy(gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (canTake)
                {
                    characterStats.GiveShield();
                    Destroy(gameObject);
                }
            }
           
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                rb.isKinematic = true;
            }
        }
    }
}
