using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OniricoStudios
{
    public class DoorTransition : MonoBehaviour
    {
        [SerializeField] Link link = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                link.GoTo();
            }
        }
    }
}
