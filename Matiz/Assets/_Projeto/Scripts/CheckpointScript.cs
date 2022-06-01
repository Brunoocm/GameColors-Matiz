using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace OniricoStudios
{
    public class CheckpointScript : MonoBehaviour
    {
        [Header("Light")]
        public Transform chromas;
        public float maxIntensity;
        public float minIntensity;
        public float speed;
        public new Light light;
        private float intensity;

        [Header("Spawn")]
        public GameObject spawnpoint;
        public bool currentSpawnpoint;
        [HideInInspector] public bool selectColor;
        private bool isActive;
        private bool saving;
        private bool isInteract;

        [Header("Events")]
        public UnityEvent m_MyEvent;

        MainCheckpoint mainCheckpoint => gameObject.GetComponentInParent<MainCheckpoint>();
        CharacterMovement characterMovement;
        CharacterStats charStats;

        void Start()
        {
            light.intensity = 0;

        }

        void Update()
        {
            light.intensity = intensity;

            if (isActive)
            {
                if (Input.GetKeyDown(KeyCode.E) && !saving && !isInteract)
                {
                    mainCheckpoint.ResetSpawnpoints();
                    StartCoroutine(SaveCoroutine());
                    StartCoroutine(ChromaAppiers());

                    currentSpawnpoint = true;
                    isInteract = true;
                    saving = true;
                }
                else if (Input.GetKeyDown(KeyCode.E) && !saving && isInteract) //clicar fora do bagulho com o mouse
                {
                    StartCoroutine(ChromaDesappiers());
                    characterMovement.canMove = true;
                    isInteract = false;
                    m_MyEvent.Invoke();
                }
            }

            if (isActive && intensity <= maxIntensity)
            {
                intensity += Time.deltaTime * speed;
            }
            else if (intensity > minIntensity)
            {
                intensity -= Time.deltaTime * speed;
            }


        }

        public IEnumerator ChromaAppiers()
        {
            selectColor = true;
            chromas.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            chromas.transform.DOScale(1, 0.4f);
        }
        public IEnumerator ChromaDesappiers()
        {
            selectColor = false;
            chromas.transform.DOScale(0, 0.4f);
            yield return new WaitForSeconds(0.4f);
            chromas.gameObject.SetActive(false);

        }

        public IEnumerator SaveCoroutine()
        {
            characterMovement.canMove = false;
            mainCheckpoint.textObj.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            saving = false;
            mainCheckpoint.textObj.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isActive = true;

                characterMovement = other.gameObject.GetComponent<CharacterMovement>();
                charStats = other.gameObject.GetComponent<CharacterStats>();
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
}