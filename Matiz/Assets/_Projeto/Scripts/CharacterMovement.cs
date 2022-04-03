using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private static readonly int WALK_PROPERTY = Animator.StringToHash("Walk");

    [SerializeField]
    private float speed = 2f;

    //private Animator anim => gameObject.GetComponent<Animator>();
    private Rigidbody rb => gameObject.GetComponent<Rigidbody>();
    private SpriteRenderer spriteRenderer => gameObject.GetComponentInChildren<SpriteRenderer>();


    private Vector3 _movement;

    private void Update()
    {
        // Vertical
        float inputY = 0;
        if (Input.GetKey(KeyCode.UpArrow))
            inputY = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            inputY = -1;

        // Horizontal
        float inputX = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputX = 1;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputX = -1;
            spriteRenderer.flipX = true;
        }

        // Normalize
        _movement = new Vector3(inputX, 0, inputY).normalized;

        //anim.SetBool(WALK_PROPERTY, Math.Abs(_movement.sqrMagnitude) > Mathf.Epsilon);
    }

    private void FixedUpdate()
    {
        rb.velocity = _movement * speed;
    }

}
