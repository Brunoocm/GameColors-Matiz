using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerPlace;
    [SerializeField] private Transform harbor;
    [SerializeField] private Transform playerHarbor;

    [HideInInspector] public bool boatCanMove;
    [HideInInspector] public bool inHarbor;
    [HideInInspector] public bool inBoat;

    private CharacterMovement player;

    [SerializeField] private Animator anim;
    CharacterController charController;
    Rigidbody rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

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
                Land();
            }
            else
            {
                Board();
            }
        }

        if (inBoat)
        {
            player.transform.position = playerPlace.position;
        }
    }

    void Board()
    {
        player.canMove = false;
        player.transform.parent = transform;
        player.anim.enabled = false;

        inBoat = true;
        boatCanMove = true;

        rb.isKinematic = false;
    }

    void Land()
    {
        player.canMove = true;
        player.transform.parent = null;
        player.transform.position = playerHarbor.position;
        player.anim.enabled = true;

        inBoat = false;
        boatCanMove = false;
        //transform.position = harbor.position;

        rb.isKinematic = true;
    }

    void BoatMove()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(xMove, 0, yMove).normalized;

        if(moveDir.magnitude > 0.9f)
        {
            //charController.Move(moveDir * speed * Time.deltaTime);
            rb.AddForce(moveDir * speed * Time.deltaTime);
        }

        anim.SetFloat("MoveX", xMove);
        anim.SetFloat("MoveZ", yMove);
        anim.SetFloat("Speed", moveDir.sqrMagnitude);
    }
}
