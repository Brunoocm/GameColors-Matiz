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
                
                //link.GoTo();
                StartCoroutine(LoadYourAsyncScene());
            }
        }


        IEnumerator LoadYourAsyncScene()
        {

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(link.nameScene());

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
