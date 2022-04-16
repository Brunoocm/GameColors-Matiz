using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerPlace;
    [SerializeField] private Transform harbor;

    //[HideInInspector] 
    public bool boatCanMove;

    //[HideInInspector] 
    public bool inHarbor;

    //[HideInInspector] 
    public bool inBoat;

    CharacterMovement player;

    //Animator anim;
    CharacterController charController;
    Rigidbody rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        //anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //if (boatCanMove) BoatMove();
    }

    private void Update()
    {
        if (boatCanMove) BoatMove();

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
    }

    void Board()
    {
        print("board");

        player.canMove = false;
        player.transform.parent = transform;
        player.transform.position = playerPlace.position;
        player.anim.enabled = false;

        inBoat = true;
        boatCanMove = true;
    }

    void Land()
    {
        print("land");

        player.canMove = true;
        player.transform.parent = null;
        player.anim.enabled = true;

        inBoat = false;
        boatCanMove = false;
        transform.position = harbor.position;

        //anim.SetBool("idle", true);
    }

    void BoatMove()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(xMove, 0, yMove).normalized;

        if(moveDir.magnitude > 0.1f)
        {
            charController.Move(moveDir * speed * Time.deltaTime);
            //rb.velocity = new Vector3(moveDir.x * speed * Time.deltaTime, 0, moveDir.y * speed * Time.deltaTime);
        }

        //anim.SetFloat("xMove", xMove);
        //anim.SetFloat("yMove", yMove);
        //anim.SetFloat("speed", moveDir.sqrMagnitude);

        print(moveDir.sqrMagnitude);
    }
}
