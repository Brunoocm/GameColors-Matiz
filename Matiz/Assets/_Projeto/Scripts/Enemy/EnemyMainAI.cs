using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace OniricoStudios
{
    [DefaultExecutionOrder(0)]
    public class EnemyMainAI : MonoBehaviour
    {
        private static EnemyMainAI _instance;
        public static EnemyMainAI Instance
        {
            get
            {
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        public List<EnemyBaseMove> Units = new List<EnemyBaseMove>();
        public List<GameObject> isSeen = new List<GameObject>();

        public Transform Target;
        public float RadiusAroundTarget = 0.5f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);


        }

        private void Update()
        {
            if (CharacterStats.playerObj != null)
            {
                MakeAgentsCircleTarget();
            }
        }


        private void MakeAgentsCircleTarget()
        {
            if(Target == null) Target = CharacterStats.playerObj.transform;
      
            for (int i = 0; i < Units.Count; i++)
            {

                if (Units[i].seesTarget && Units[i] != null)
                {
                    if (!isSeen.Contains(Units[i].gameObject))
                    {
                        isSeen.Add(Units[i].gameObject);
                    }
                }
                else if (isSeen.Contains(Units[i].gameObject) || isSeen.Any(i => i == null) && Units[i] != null)
                {
                    isSeen.Remove(Units[i].gameObject);
                }

                if (Units[i].navMeshAgent.enabled && Units[i] != null)
                {
                    Units[i].FollowTarget(new Vector3(
                     Target.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / Units.Count),
                     Target.position.y,
                     Target.position.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / Units.Count)
                     ));
                }

            }

           
        }

        public void DeleteObj(GameObject obj, EnemyBaseMove s)
        {
            isSeen.Remove(obj);
            Units.Remove(s);
        }
    }
}
