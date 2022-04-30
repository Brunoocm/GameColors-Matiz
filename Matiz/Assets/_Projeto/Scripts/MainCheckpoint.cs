using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCheckpoint : MonoBehaviour
{
    public CheckpointScript[] checkpoints;
    public GameObject playerObj;
    GameObject currentSpawnpoint;

    CinemachineVirtualCamera cinemachineVirtualCamera => FindObjectOfType<CinemachineVirtualCamera>();
    void Start()
    {
        
    }

    void Update()
    {
        print(currentSpawnpoint);
        if(Input.GetKeyDown(KeyCode.R))
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
            if(checkpoints[i].currentSpawnpoint)
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
            Instantiate(playerObj, currentSpawnpoint.GetComponent<CheckpointScript>().spawnpoint.position, Quaternion.identity);
        }
        else
        {
            Destroy(FindObjectOfType<CharacterStats>().gameObject);
            Transform pos = currentSpawnpoint.GetComponent<CheckpointScript>().spawnpoint;

            yield return new WaitForSeconds(1f);
            cinemachineVirtualCamera.Follow = pos;
            yield return new WaitForSeconds(1f);

            GameObject player = Instantiate(playerObj, pos.position, Quaternion.identity);
            cinemachineVirtualCamera.Follow = player.transform;
            cinemachineVirtualCamera.LookAt = player.transform;
            cinemachineVirtualCamera.transform.eulerAngles = new Vector3(49, 0, 0);
        }
    }
}
