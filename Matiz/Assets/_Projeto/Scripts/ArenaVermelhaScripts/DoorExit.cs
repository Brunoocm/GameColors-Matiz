using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace OniricoStudios
{
    public class DoorExit : MonoBehaviour
    {
        Image transition => gameObject.GetComponentInChildren<Image>();
        public Transform pos;

        [SerializeField] Link link = null;
        void Awake()
        {
            if (Link.current == link)
            {
                StartCoroutine(ReloadPosition());
            }
        }

        private void Spawn()
        {
            CharacterStats.playerObj.transform.position = pos.transform.position;
        }

        IEnumerator ReloadPosition()
        {
            Spawn();    
            transition.transform.DOScale(new Vector2(11, 11), 0.01f);
            yield return new WaitForSeconds(1f);
            Spawn();
            transition.transform.DOScale(new Vector2(0, 0), 1);
        }
    }
}
