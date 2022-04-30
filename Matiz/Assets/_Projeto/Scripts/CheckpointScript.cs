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
    private bool isActive;
    void Start()
    {
        light.intensity = 0;

    }

    void Update()
    {
        light.intensity = intensity;

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
