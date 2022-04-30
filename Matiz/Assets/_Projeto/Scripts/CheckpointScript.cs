using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [Header("Light")]
    public float maxIntensity;
    public float minIntensity;
    public float speed;
    public Light light;
    private float intensity;

    [Header("Spawn")]
    public Transform spawnpoint;
    public bool currentSpawnpoint;
    private bool isActive;
    

    MainCheckpoint mainCheckpoint => gameObject.GetComponentInParent<MainCheckpoint>();

    void Start()
    {
        light.intensity = 0;

    }

    void Update()
    {
        light.intensity = intensity;

        if(isActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                mainCheckpoint.ResetSpawnpoints();
                currentSpawnpoint = true;
            }
        }

        if (isActive && intensity <= maxIntensity)
        {
            intensity += Time.deltaTime * speed;
        }
        else if(intensity > minIntensity)
        {
            intensity -= Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
        }
    }
}
