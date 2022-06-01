using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

namespace OniricoStudios
{
    public class WavesByTime : MonoBehaviour
    { //"Editor" not "Engine"


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

        private int numobj;

        ProgressionManager progressionManager => FindObjectOfType<ProgressionManager>();

        public NavMeshSurface navMesh;
        void Awake()
        {
            navMesh.BuildNavMesh();

      
        }

        void Start()
        {
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

            m_timerWaves = timerWaves;
            door.SetActive(false);
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

            if (num == EnemiesParent.Length)
            {
                isFinished = true;
            }

            if (isFinished)
            {
                door.SetActive(true);

                if (firstArena) //completou a primeira arena
                {
                    progressionManager.secondArena = true; // segunda arena abre

                }
                else if(secondArena)
                {
                   
                }
            }
        }

        IEnumerator StartWaves()
        {
            yield return new WaitForSeconds(1);
            isStarted = true;
        }

        public Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            float Func(float x) => 4 * (-height * x * x + height * x);

            var mid = Vector3.Lerp(start, end, t);



            return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);

        }

        IEnumerator Ground()
        {
            yield return new WaitForSeconds(1.6f);
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