using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismaBase : MonoBehaviour
{
    public float timeToAppers; //tempo para voltar o prisma, mesmo que o jogador nao pegue
    public float force;
    public GameObject prismaVisual;

    Rigidbody rb => gameObject.GetComponent<Rigidbody>();
    void Start()
    {
        DropObject();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        float angle = Random.Range(0, 360);
        Vector3 angleend = new Vector3(angle, angle, angle);
        print(angle);
        rb.AddForce(new Vector3(force, force, force), ForceMode.Impulse);
        rb.AddTorque(angleend, ForceMode.Impulse);
    }
}
