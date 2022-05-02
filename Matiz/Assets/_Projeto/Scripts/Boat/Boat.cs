using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class Boat : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform seat;

        [HideInInspector] public bool boatCanMove;
        [HideInInspector] public bool inHarbor;
        [HideInInspector] public bool inBoat;
        [HideInInspector] public Transform land;

        private CharacterMovement charMove;
        private CharacterStats charStats;

        [SerializeField] private Animator anim;
        CharacterController charController;
        Rigidbody rb;

        private void Start()
        {
            charMove = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
            charStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();

            charController = GetComponent<CharacterController>();
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (boatCanMove) BoatMove();
        }

        private void Update()
        {
            //if (boatCanMove) BoatMove();

            if (inHarbor && Input.GetKeyDown(KeyCode.E))
            {
                charMove = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
                charStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();

                if (inBoat)
                {
                    Land();
                }
                else
                {
                    Board();
                }
            }

            if (inBoat)
            {
                charMove.transform.position = new Vector3(seat.position.x, seat.position.y, seat.position.z);
            }
        }

        void Board()
        {
            charMove.canMove = false;
            charMove.transform.parent = transform;
            charMove.anim.enabled = false;

            charStats.canUseSkill = false;

            inBoat = true;
            boatCanMove = true;

            rb.isKinematic = false;
        }

        void Land()
        {
            charMove.canMove = true;
            charMove.transform.parent = null;
            charMove.transform.position = land.position;
            charMove.anim.enabled = true;

            charStats.canUseSkill = true;

            inBoat = false;
            boatCanMove = false;

            rb.isKinematic = true;
        }

        void BoatMove()
        {
            float xMove = Input.GetAxisRaw("Horizontal");
            float yMove = Input.GetAxisRaw("Vertical");

            Vector3 moveDir = new Vector3(xMove, 0, yMove).normalized;

            if (moveDir.magnitude > 0.9f)
            {
                //charController.Move(moveDir * speed * Time.deltaTime);
                rb.AddForce(moveDir * speed * Time.deltaTime);
            }

            anim.SetFloat("MoveX", xMove);
            anim.SetFloat("MoveZ", yMove);
            anim.SetFloat("Speed", moveDir.sqrMagnitude);
        }
    }
}