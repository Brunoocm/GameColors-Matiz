using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;

namespace OniricoStudios
{
    public class MainCheckpoint : MonoBehaviour
    {
        public CheckpointScript[] checkpoints;
        public GameObject playerObj;
        public GameObject textObj;
        public GameObject startGamePos;
        static GameObject currentSpawnpoint;

        Image transition => gameObject.GetComponentInChildren<Image>();
        CinemachineVirtualCamera cinemachineVirtualCamera => FindObjectOfType<CinemachineVirtualCamera>();
        [HideInInspector] public CharacterMovement characterMovement;
        void Start()
        {
            characterMovement = FindObjectOfType<CharacterMovement>();
            currentSpawnpoint = startGamePos;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Death();
            }
        }

        public void ResetSpawnpoints()
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                checkpoints[i].currentSpawnpoint = false;
            }
        }

        public void Death()
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].currentSpawnpoint)
                {
                    currentSpawnpoint = checkpoints[i].gameObject;

                    StartCoroutine(SpawnPlayer());

                }
                else
                {
               
                }
            }
        }

        public IEnumerator SpawnPlayer()
        {
            if (FindObjectOfType<CharacterStats>() == null)
            {
                Instantiate(playerObj, currentSpawnpoint.GetComponent<CheckpointScript>().spawnpoint.transform.position, Quaternion.identity);
            }
            else
            {
                Destroy(FindObjectOfType<CharacterStats>().gameObject);
                GameObject pos = currentSpawnpoint.GetComponent<CheckpointScript>().spawnpoint;

                transition.transform.DOScale(new Vector2(11, 11), 1);
                transition.DOFade(1, 1);

                yield return new WaitForSeconds(3);
                cinemachineVirtualCamera.Follow = pos.transform;
                yield return new WaitForSeconds(1f);

                transition.transform.DOScale(new Vector2(0, 0), 1);

                GameObject player = Instantiate(playerObj, pos.transform.position, Quaternion.identity);
                characterMovement = player.GetComponent<CharacterMovement>();
                cinemachineVirtualCamera.Follow = player.transform;
                cinemachineVirtualCamera.LookAt = player.transform;
                cinemachineVirtualCamera.transform.eulerAngles = new Vector3(49, 0, 0);

                yield return new WaitForSeconds(1f);
                transition.DOFade(0, 0);

            }
        }
    }
}