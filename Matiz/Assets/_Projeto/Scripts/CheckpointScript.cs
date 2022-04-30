using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckpointScript : MonoBehaviour
{
    [Header("Light")]
    public Transform chromas;
    public float maxIntensity;
    public float minIntensity;
    public float speed;
    public Light light;
    private float intensity;

    [Header("Spawn")]
    public GameObject spawnpoint;
    public bool currentSpawnpoint;
    private bool isActive;
    private bool saving;
    

    MainCheckpoint mainCheckpoint => gameObject.GetComponentInParent<MainCheckpoint>();
    CharacterMovement characterMovement;

    void Start()
    {
        light.intensity = 0;

    }

    void Update()
    {
        light.intensity = intensity;

        if(isActive)
        {
            if(Input.GetKeyDown(KeyCode.E) && !saving)
            {
                mainCheckpoint.ResetSpawnpoints();
                StartCoroutine(SaveCoroutine());
                StartCoroutine(ChromaAppiers());

                currentSpawnpoint = true;
                saving = true;
            }
            else if(Input.GetKeyDown(KeyCode.S) && !saving)
            {
                StartCoroutine(ChromaDesappiers());
                mainCheckpoint.characterMovement.canMove = true;
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

    public IEnumerator ChromaAppiers()
    {
        chromas.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        chromas.transform.DOScale(1, 0.4f);
    }   
    public IEnumerator ChromaDesappiers()
    {
        chromas.transform.DOScale(0, 0.4f);
        yield return new WaitForSeconds(0.4f);
        chromas.gameObject.SetActive(false);

    }

    public IEnumerator SaveCoroutine()
    {
        mainCheckpoint.characterMovement.canMove = false;
        mainCheckpoint.textObj.SetActive(true);
        yield return new WaitForSeconds(3f);
        saving = false;
        mainCheckpoint.textObj.SetActive(false);
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
