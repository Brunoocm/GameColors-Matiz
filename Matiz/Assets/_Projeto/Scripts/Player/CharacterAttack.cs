using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    
    public GameObject attack;

    public LayerMask groundLayer;
    public float timeBTWAttack;
    public float timeAttack;
    public float force;

    [HideInInspector]
    public bool canAttack;

    CharacterMovement characterMovement => gameObject.GetComponent<CharacterMovement>();
    Transform playerPos => gameObject.GetComponent<Transform>();
    Animator anim => gameObject.GetComponentInChildren<Animator>();
    Rigidbody rb => gameObject.GetComponent<Rigidbody>();
    void Start()
    {
        canAttack = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack && characterMovement.canMove && characterMovement.canDash)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) // 6 = layermask ground
            {
                AttackPlayerOption2(hit);

            }
        }
    }
    void AttackPlayerOption2(RaycastHit mousePos)
    {
        canAttack = false;

        anim.SetTrigger("AttackTrigger");
        attack.SetActive(true);

        float vertical = mousePos.point.z - playerPos.position.z;
        float horizontal = mousePos.point.x - playerPos.position.x;

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);

        SprintLR(horizontal, vertical);
    }

    public void SprintLR(float dirH, float dirV)
    {
        StartCoroutine(KnockbackLR(dirH, dirV));
    } 


    public IEnumerator KnockbackLR(float dirH, float dirV)
    {
        characterMovement.canMove = false;

        float startTime = Time.time;
        while (Time.time < startTime + timeAttack)
        {
            transform.Translate(new Vector3(dirH,0, dirV).normalized * force * Time.deltaTime);
            yield return null;
        }

        attack.SetActive(false);
        characterMovement.canMove = true;

        yield return new WaitForSeconds(timeBTWAttack);
        canAttack = true;
    }
}
