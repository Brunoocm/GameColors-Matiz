using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class EnemyAttackMinion : MonoBehaviour
    {
        [Header("Stats")]
        public int damage;
        public float antecipation;

        [Header("Attack")]
        public GameObject pivotAttack;
        public float dashForce;
        public float timeDash;
        public float cdToDash;


        [Header("Target")]
        public string tagNameTarget;
        private bool oneTime;
        private bool hitTarget;
        private float angle;
        void Start()
        {
        }

        void Update()
        {


     
        }

        public void AttackVoid()
        {
            if (!oneTime)
            {
                if (CharacterStats.playerObj != null) StartCoroutine(Dash(CharacterStats.playerObj.transform));
            }
        }

        public void HitTarget(GameObject target)
        {
            hitTarget = true;
            if (CharacterStats.playerObj)
            {
                CharacterStats.playerObj.DamageVoid(damage, transform);
            }
        }

        public IEnumerator Dash(Transform dir)
        {
            oneTime = true;

            yield return new WaitForSeconds(antecipation);

            float vertical = dir.position.z - transform.position.z;
            float horizontal = dir.position.x - transform.position.x;

            float startTime = Time.time;
            while (Time.time < startTime + timeDash && !hitTarget)
            {
                transform.Translate(new Vector3(horizontal, 0, vertical).normalized * dashForce * Time.deltaTime);
                yield return null;
            }

            FinalAttack();

            yield return new WaitForSeconds(0.2f);

            pivotAttack.SetActive(false);

            yield return new WaitForSeconds(cdToDash);

            

            oneTime = false;
            hitTarget = false;
        }

        public void FinalAttack()
        {
            pivotAttack.SetActive(true);

            Vector3 difference = CharacterStats.playerObj.transform.position - pivotAttack.transform.position;
            angle = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            pivotAttack.transform.eulerAngles = new Vector3(0, angle + 180, 0);


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagNameTarget))
            {
                HitTarget(other.gameObject);
                StartCoroutine(Knock(other.transform, 0.1f));
            }
        }   
        

        public IEnumerator Knock(Transform dir, float timeKnock)
        {

            float vertical = dir.position.z - transform.position.z;
            float horizontal = dir.position.x - transform.position.x;

            float startTime = Time.time;
            while (Time.time < startTime + timeKnock)
            {

                transform.Translate(new Vector3(-horizontal, 0, -vertical).normalized * dashForce / 2 * Time.deltaTime);
                yield return null;
            }


        }

    }
}