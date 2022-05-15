using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class Harbor : MonoBehaviour
    {
        [SerializeField] private Transform land;
        [SerializeField] private Transform ocean;

        private Boat boat;

        void Start()
        {
            boat = FindObjectOfType<Boat>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!boat.inBoat)
                {
                    boat.gameObject.SetActive(false);
                    boat.gameObject.SetActive(true);
                }

                boat.charMove = other.gameObject.GetComponent<CharacterMovement>();
                boat.charStats = other.gameObject.GetComponent<CharacterStats>();
                boat.player = other.gameObject;

                boat.inHarbor = true;
                boat.land = land;
                

                if(!boat.inBoat)
                {
                    boat.transform.position = ocean.position;
                }

               
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
               
                boat.inHarbor = false;
                boat.land = null;
            }
        }
    }
}
