using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
namespace OniricoStudios
{
    public class WavesByTime : MonoBehaviour
    {
        public bool firstArena, secondArena;
        public GameObject door;

        public float timerWaves;
        private float m_timerWaves;
        private int num;

        public GameObject[] EnemiesObj;
        public GameObject[] EnemiesParent;

        [SerializeField] public List<GameObject> enemy;
        [SerializeField] public List<Transform> PosSpawn;
        [SerializeField] public List<Transform> PosToJump;


        private bool isStarted;
        private bool isFinished;
        private bool oneTime;

        private int numobj;

        ProgressionManager progressionManager => FindObjectOfType<ProgressionManager>();
        CompleteDesafioUI completeDesafioUI => FindObjectOfType<CompleteDesafioUI>();
        void Awake()
        {

          
        }

        void Start()
        {
            m_timerWaves = timerWaves;
            door.SetActive(false);

            for (int i = 0; i < EnemiesParent.Length; i++)
            {
                PosSpawn.Add(EnemiesParent[i].transform.GetChild(0));
                PosToJump.Add(EnemiesParent[i].transform.GetChild(1));

                int num = Random.Range(0, EnemiesObj.Length);
                GameObject e = Instantiate(EnemiesObj[i], PosSpawn[i].position, Quaternion.identity);
                e.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                e.gameObject.GetComponent<EnemyBaseMove>().isStopped = true;
                enemy.Add(e);

            }

            StartCoroutine(StartWaves());
        }

        void Update()
        {
           
            if (isStarted)
            {
                if (m_timerWaves <= 0 && num < EnemiesParent.Length)
                {
                    num++;

                    DOTween.To(setter: value =>
                    {
                        enemy[num - 1].transform.position = Parabola(PosSpawn[num - 1].position, PosToJump[num - 1].position, 10, value);
                    }, startValue: 0, endValue: 1, duration: 1f)
                .SetEase(Ease.Linear);



                    StartCoroutine(Ground());

                    m_timerWaves = timerWaves;

                }
                else
                {
                    m_timerWaves -= Time.deltaTime;
                }
            }

            if (FindObjectOfType<EnemyHealth>() == null)
            {
                isFinished = true;
            }

            if (isFinished && !oneTime)
            {

                StartCoroutine(TimeToEnd());
                if (firstArena) //completou a primeira arena
                {
                    progressionManager.unlockSecondArena = true; //segunda arena abre
                  
                    StartCoroutine(completeDesafioUI.SetInsigniaArena());
                }
                else if(secondArena)
                {
                    progressionManager.unlockFinal = true; //final abre

                    StartCoroutine(completeDesafioUI.SetInsigniaArena2());
                }
                oneTime = true;
                //transform.gameObject.SetActive(false);
            }
        }

        IEnumerator StartWaves()
        {
            yield return new WaitForSeconds(0.5f);
            isStarted = true;
        }
        IEnumerator TimeToEnd()
        {
            yield return new WaitForSeconds(5);
            door.SetActive(true);
        }

        public Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            float Func(float x) => 4 * (-height * x * x + height * x);

            var mid = Vector3.Lerp(start, end, t);



            return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);

        }

        IEnumerator Ground()
        {
            yield return new WaitForSeconds(1);
            enemy[num - 1].gameObject.GetComponent<NavMeshAgent>().enabled = true;
            enemy[num - 1].gameObject.GetComponent<EnemyBaseMove>().isStopped = false;
        }

        //public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
        //{
        //    float Func(float x) => 4 * (-height * x * x + height * x);

        //    var mid = Vector2.Lerp(start, end, t);

        //    return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
        //}
    }
}