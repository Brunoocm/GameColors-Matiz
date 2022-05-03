using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class CharacterMovement : MonoBehaviour
    {

        public float timeBTWDash;
        public float timeDash;
        public float forceDash;

        public LayerMask groundLayer;

        [HideInInspector]
        public bool canMove;
        [HideInInspector]
        public bool canDash;

        public float speed = 2f;
        [HideInInspector] public float m_speed;

        public float trailCooldown;
        public GameObject trailFX;
        public Transform feet;

        private bool dashing;
        private float dashMeter;
        private float xMove, yMove;
        private float trailTime;
        private float vSpeed;
        Vector3 lastDir;
        Vector3 moveDir;

        private Rigidbody rb => gameObject.GetComponent<Rigidbody>();
        private SpriteRenderer spriteRenderer => gameObject.GetComponentInChildren<SpriteRenderer>();
        private CharacterController characterController => gameObject.GetComponent<CharacterController>();
        private CharacterAttack characterAttack => gameObject.GetComponent<CharacterAttack>();
        private CharacterAbilities characterAbilities => gameObject.GetComponent<CharacterAbilities>();
        private CharacterStats characterStats => gameObject.GetComponent<CharacterStats>();
        [HideInInspector] public Animator anim => gameObject.GetComponentInChildren<Animator>();

        private Vector3 _movement;

        private Boat boat;

        private void Start()
        {
            canMove = true;
            canDash = true;

            boat = FindObjectOfType<Boat>();

            m_speed = speed;
        }
        private void Update()
        {
            Gravity();

            if (Input.GetKeyDown(KeyCode.Space) && !characterAbilities.vermelhoTrue)
            {
                if (canMove && canDash)
                {
                    StartCoroutine(Dash(timeDash));
                }
            }


            //print(dashMeter);

            if (characterAbilities.vermelhoTrue)
            {
                if (Input.GetKey(KeyCode.Space) && dashMeter < characterAbilities.vermelhoAbility.dashMaxForce)
                {
                    canMove = false;

                    dashMeter += Time.deltaTime / 5;
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (dashMeter >= characterAbilities.vermelhoAbility.dashMinForce)
                    {
                        StartCoroutine(Dash(dashMeter));
                    }
                    else
                    {
                        canMove = true;
                    }
                }
            }
        }

        void Gravity()
        {
           
            //Vector3 vel = transform.forward * Input.GetAxis("Vertical") * speed;
            //var controller = GetComponent(CharacterController);

            vSpeed -= 1 * Time.deltaTime;
            //vel.y = vSpeed; // include vertical speed in vel
                            // convert vel to displacement and Move the character:
            characterController.Move(new Vector3(0,-10,0) * Time.deltaTime);

        }

        void PlayerMove(float xMove, float yMove)
        {
            if (moveDir.magnitude > 0.1f)
            {
                characterController.Move(moveDir * speed * Time.deltaTime);

                trailTime -= Time.deltaTime;

                if (trailTime <= 0)
                {
                    Instantiate(trailFX, feet.position, Quaternion.identity);
                    trailTime = trailCooldown;
                }
            }

            anim.SetFloat("MoveX", xMove);
            anim.SetFloat("MoveZ", yMove);
            anim.SetFloat("Speed", moveDir.sqrMagnitude);
        }

        public IEnumerator Dash(float dashTime)
        {
            dashMeter = 0;

            if (characterAbilities.cinzaTrue || characterAbilities.vermelhoTrue)
            {
                characterStats.timeInvencible = characterStats.m_timeInvencible;
                characterStats.canDamage = false;
            }

            canMove = false;
            canDash = false;

            float xMove = Input.GetAxisRaw("Horizontal");
            float yMove = Input.GetAxisRaw("Vertical");
            Vector3 moveDir = new Vector3(xMove, 0f, yMove).normalized;

            anim.SetTrigger("DashTrigger");
            anim.SetFloat("Vertical", yMove);
            anim.SetFloat("Horizontal", xMove);

            float startTime = Time.time;
            while (Time.time < startTime + dashTime)
            {
                if (characterAbilities.vermelhoTrue)
                {
                    Instantiate(characterAbilities.vermelhoAbility.dashFX, feet.position, Quaternion.identity);
                }

                if (characterAbilities.cinzaTrue)
                {
                    Instantiate(characterAbilities.cinzaAbility.dashFX, feet.position, Quaternion.identity);
                }

                dashing = true;
                //transform.Translate(moveDir * forceDash * Time.deltaTime);
                transform.Translate(lastDir * forceDash * Time.deltaTime);
                yield return null;
            }

            canMove = true;

            yield return new WaitForSeconds(timeBTWDash);
            canDash = true;
            dashing = false;
        }
        private void FixedUpdate()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            moveDir = new Vector3(horizontal, 0f, vertical).normalized;

            if (canMove) PlayerMove(horizontal, vertical);

            if (moveDir != Vector3.zero)
            {
                lastDir = moveDir;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Harbor"))
            {
                boat.inHarbor = true;
            }

            if (other.gameObject.CompareTag("Enemy") && other.GetComponent<EnemyHealth>() != null && dashing)
            {
                if (characterAbilities.cinzaTrue)
                {
                    other.GetComponent<EnemyHealth>().DamageVoid(0);
                }
                else if (characterAbilities.vermelhoTrue)
                {
                    other.GetComponent<EnemyHealth>().DamageVoid(characterAbilities.vermelhoAbility.dashDamage);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Harbor"))
            {
                boat.inHarbor = false;
            }
        }
    }
}