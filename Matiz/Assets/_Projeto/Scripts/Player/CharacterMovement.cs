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

        //[HideInInspector]
        public bool canMove;
        [HideInInspector]
        public bool canDash;

        public float speed = 2f;
        public float footstepSfxTime;
        [HideInInspector] public float m_speed;

        public float trailCooldown;
        public GameObject trailFX;
        public Transform feet;

        [HideInInspector] public bool dashing;
        public bool hasWall;
        private float dashMeter;
        private float xMove, yMove;
        private float trailTime;
        private float vSpeed;
        Vector3 lastDir;
        Vector3 moveDir;
        Vector3 final;

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

            InvokeRepeating("SoundFootSteps", 0, footstepSfxTime);
        }
        private void Update()
        {
            Gravity();



            if (Input.GetKeyDown(KeyCode.Space) && characterAbilities.cinzaTrue)
            {
                if (canMove && canDash)
                {
                    StartCoroutine(Dash(timeDash));
                }
            }


            //print(dashMeter);

            if (characterAbilities.vermelhoTrue)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (dashMeter < characterAbilities.vermelhoAbility.dashMaxForce)
                    {
                        dashMeter += Time.deltaTime / 3;
                    }

                    float slowMo = m_speed * 0.25f;

                    speed = slowMo;

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

        void SoundFootSteps()
        {
            if (moveDir != Vector3.zero)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.playerStepsEvent);
            }
        }

        void Gravity()
        {

            //Vector3 vel = transform.forward * Input.GetAxis("Vertical") * speed;
            //var controller = GetComponent(CharacterController);

            vSpeed -= 1 * Time.deltaTime;
            //vel.y = vSpeed; // include vertical speed in vel
            // convert vel to displacement and Move the character:
            characterController.Move(new Vector3(0, -10, 0) * Time.deltaTime);

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

            //hasWall = Physics.Raycast(transform.position, final * 10, Mathf.Infinity, 10);

            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position, final.normalized * (forceDash * timeDash), out hitInfo, (forceDash * timeDash)))
            {
                if (hitInfo.collider.gameObject.tag == "Walls")
                {
                    hasWall = true;
                }


            }
            else
            {
                hasWall = false;
            }

        }
        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, final.normalized * (forceDash * timeDash), Color.green);
        }
        //private bool hasWall()
        //{
        //    return Physics.Raycast(transform.position, dir, distance, 10);
        //}

        public IEnumerator Dash(float dashTime)
        {
            //if (!hasWall)
            //{

            dashMeter = 0;

            if (characterAbilities.cinzaTrue)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.greyDashEvent);
            }

            if (characterAbilities.vermelhoTrue)
            {
                //FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.redDashEvent);
            }

            if (!characterAbilities.vermelhoTrue)
            {
                canMove = false;
            }

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
                characterStats.timeInvencible = characterStats.m_timeInvencible;
                characterStats.canDamage = false;

                if (characterAbilities.vermelhoTrue)
                {
                    Instantiate(characterAbilities.vermelhoAbility.dashFX, feet.position, Quaternion.identity);
                    transform.Translate(lastDir * forceDash * characterAbilities.vermelhoAbility.dashSpeed * Time.deltaTime);
                }

                if (characterAbilities.cinzaTrue)
                {
                    Instantiate(characterAbilities.cinzaAbility.dashFX, feet.position, Quaternion.identity);
                    transform.Translate(lastDir * forceDash * Time.deltaTime);
                }

                dashing = true;
                //transform.Translate(moveDir * forceDash * Time.deltaTime);
                yield return null;
            }

            characterStats.timeInvencible = 0;
            characterStats.canDamage = true;

            canMove = true;
            dashing = false;

            yield return new WaitForSeconds(timeBTWDash);

            canDash = true;
            //}
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

            final = new Vector3(horizontal, 0f, vertical);

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
                    if (dashing)
                    {
                        other.GetComponent<EnemyHealth>().currentStacks++;
                        other.GetComponent<EnemyHealth>().GreyPassiveSkill();
                    }
                }
                else if (characterAbilities.vermelhoTrue)
                {
                    if (dashing)
                    {
                        other.GetComponent<EnemyHealth>().DamageVoid(characterAbilities.vermelhoAbility.dashDamage);
                    }
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