using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class CharacterAttack : MonoBehaviour
    {
        public GameObject attack;
        public GameObject pivotAttack;

        //public float xaa, yaa;
        //public float offset;
        public LayerMask groundLayer;
        public float timeBTWAttack;
        public float timeAttack;
        public float force;

        [HideInInspector]
        public bool canAttack;

        bool invert;

        private float angle;
        CharacterMovement characterMovement => gameObject.GetComponent<CharacterMovement>();
        CharacterAbilities characterAbilities => gameObject.GetComponent<CharacterAbilities>();
        Transform playerPos => gameObject.GetComponent<Transform>();
        Animator anim => gameObject.GetComponentInChildren<Animator>();
        Rigidbody rb => gameObject.GetComponent<Rigidbody>();





        Vector3 orgSize;


        void Start()
        {
            canAttack = true;

            orgSize = transform.localScale;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && canAttack && characterMovement.canMove && !characterMovement.dashing)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) // 6 = layermask ground
                {
                    AttackPlayer(hit);

                    Vector3 difference = hit.point- attack.transform.position;
                    angle = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
                    attack.transform.eulerAngles = new Vector3(-90, angle + 190, 0);
                    pivotAttack.transform.eulerAngles = new Vector3(-90, angle + 190, 0);
                }
            }
        }
        void AttackPlayer(RaycastHit mousePos)
        {
            canAttack = false;

            if(invert)
            {
                transform.localScale = new Vector3(orgSize.x, orgSize.y, orgSize.z);
            }
            else
            {
                transform.localScale = new Vector3(-orgSize.x, orgSize.y, orgSize.z);
            }

            invert = !invert;

            anim.SetTrigger("AttackTrigger");

            float vertical = mousePos.point.z - playerPos.position.z;
            float horizontal = mousePos.point.x - playerPos.position.x;

            anim.SetFloat("Vertical", vertical);
            anim.SetFloat("Horizontal", horizontal);

            attack.SetActive(true);
            pivotAttack.SetActive(true);
            attack.GetComponent<Animator>().Play("trail-attack");

            FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.playerAttackEvent, transform.position);

            SprintLR(horizontal, vertical);
        }

        public void SprintLR(float dirH, float dirV)
        {
            StartCoroutine(KnockbackLR(dirH, dirV));
        }


        public IEnumerator KnockbackLR(float dirH, float dirV)
        {
            //StartCoroutine(DisableAttack());

            characterMovement.canMove = false;

            float startTime = Time.time;
            while (Time.time < startTime + timeAttack)
            {
                
                transform.Translate(new Vector3(dirH, 0, dirV).normalized * force * Time.deltaTime);
                yield return null;
            }

            attack.SetActive(false);
            pivotAttack.SetActive(false);
            characterMovement.canMove = true;

            transform.localScale = orgSize;

            yield return new WaitForSeconds(timeBTWAttack);
            canAttack = true;
        }

        public void DisableAttack()
        {
            attack.SetActive(false);
        }
    }
}