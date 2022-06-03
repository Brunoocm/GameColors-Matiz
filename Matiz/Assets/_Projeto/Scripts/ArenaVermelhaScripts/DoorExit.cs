using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace OniricoStudios
{
    public class DoorExit : MonoBehaviour
    {
        Image transition => gameObject.GetComponentInChildren<Image>();
        public Transform pos;

        [SerializeField] Link link = null;
        private  Scene currentScene;

        void Awake()
        {
            if (Link.current == link)
            {
                StartCoroutine(ReloadPosition());
            }
        }
        private void Start()
        {
            currentScene = SceneManager.GetActiveScene();
        }
        private void Spawn()
        {
            CharacterStats.playerObj.transform.position = pos.transform.position;
        }

        IEnumerator ReloadPosition()
        {
            //while (SceneManager.GetActiveScene().name != "MainLand")
            //{
            //    yield return null;
            //}






            transition.transform.DOScale(new Vector2(11, 11), 0.01f);
            Spawn();
            yield return new WaitForSeconds(1f);
            Spawn();
            transition.transform.DOScale(new Vector2(0, 0), 1);
        }
    }
}
