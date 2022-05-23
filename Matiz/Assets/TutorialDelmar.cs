using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace OniricoStudios
{


    public class TutorialDelmar : MonoBehaviour
    {
        private static TutorialDelmar playerInstance;

        public Transform CinzaPos;
        public Transform VermelhaPos;
        public BlockWayArena blockWayArena;

        void Awake()
        {
            DontDestroyOnLoad(this);

            if (playerInstance == null)
            {
                playerInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))//cinza
            {
                StartCoroutine(loadsceneName("MainLand", CinzaPos));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))//vermalha
            {
                StartCoroutine(loadsceneName("MainLand", VermelhaPos));
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))//arena
            {
                SceneManager.LoadScene("PrimeiraArena");
            }
            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                BlockWayArena.firstArena = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                BlockWayArena.secondArena = true;
            }
        }

        IEnumerator loadsceneName(string name, Transform pos)

        {
            SceneManager.LoadScene(name);
            while (SceneManager.GetActiveScene().name != name)
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.5F);
            CharacterStats.playerObj.gameObject.transform.position = pos.position;
        }
    }
}
