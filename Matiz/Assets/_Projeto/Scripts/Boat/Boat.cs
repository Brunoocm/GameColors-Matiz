using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

        [HideInInspector] public GameObject player;
        [HideInInspector] public CharacterMovement charMove;
        [HideInInspector] public CharacterStats charStats;

        [SerializeField] private Animator anim;

        CharacterController charController;
        CinemachineVirtualCamera cinemachineVirtualCamera;
        Rigidbody rb;

        private void Start()
        {
            cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();


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

                if (inBoat)
                {
                    StartCoroutine(Land());
                }
                else
                {
                    Board();

                }
            }

            if (inBoat)
            {
                player.transform.position = seat.transform.position;

            }
        }

        void Board()
        {
            var transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = new Vector3(0, 30,-12);

            charMove.canMove = false;
            //charMove.transform.parent = transform;
            charMove.anim.enabled = false;

            charStats.canUseSkill = false;

            inBoat = true;
            boatCanMove = true;

            rb.isKinematic = false;
        }

        IEnumerator Land()
        {
            var transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = new Vector3(0, 19, -12);

            inBoat = false;
            boatCanMove = false;
            rb.isKinematic = true;

            charMove.transform.position = land.position;


            yield return new WaitForSeconds(0.5f);
            charStats.canUseSkill = true;
            charMove.canMove = true;
            charMove.anim.enabled = true;


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