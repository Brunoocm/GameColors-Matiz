using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private float speed = 2f;

    private Rigidbody rb => gameObject.GetComponent<Rigidbody>();
    private SpriteRenderer spriteRenderer => gameObject.GetComponentInChildren<SpriteRenderer>();
    private CharacterController characterController => gameObject.GetComponent<CharacterController>();
    private CharacterAttack characterAttack => gameObject.GetComponent<CharacterAttack>();
    private Animator anim => gameObject.GetComponentInChildren<Animator>();

    private Vector3 _movement;

    private void Start()
    {
        canMove = true;
        canDash = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canMove && canDash)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) // 6 = layermask ground
            {

                StartCoroutine(Dash(hit));

            }
        }
    }

    void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;

        if(dir.magnitude >0.1f)
        {
            characterController.Move(dir * speed * Time.deltaTime);
        }

        anim.SetFloat("MoveX", horizontal);
        anim.SetFloat("MoveZ", vertical);
        anim.SetFloat("Speed", dir.sqrMagnitude);
    }

    public IEnumerator Dash(RaycastHit dir)
    {
        canMove = false;
        canDash = false;

        float vertical = dir.point.z - transform.position.z;
        float horizontal = dir.point.x - transform.position.x;

        anim.SetTrigger("DashTrigger");
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);

        float startTime = Time.time;
        while (Time.time < startTime + timeDash)
        {
            transform.Translate(new Vector3(horizontal, 0, vertical).normalized * forceDash * Time.deltaTime);
            yield return null;
        }

        canMove = true;

        yield return new WaitForSeconds(timeBTWDash);
        canDash = true;
    }
    private void FixedUpdate()
    {
        if (canMove) PlayerMove();
       
    }

}
