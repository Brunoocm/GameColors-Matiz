using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harbor : MonoBehaviour
{
    [SerializeField] private Transform land;
    [SerializeField] private Transform ocean;

    private Boat boat;

    void Start()
    {
        boat = FindObjectOfType<Boat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boat.inHarbor = true;
            boat.land = land;
            boat.transform.position = ocean.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boat.inHarbor = false;
            boat.land = null;
        }
    }
}
