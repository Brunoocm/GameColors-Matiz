using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraZoomOut : MonoBehaviour
{

    public float offSetY;
    private float StartOffSetY;

    private bool zoomming;
    private bool current;

    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineTransposer transposer;
    public CameraZoomOut[] cameraZoomOut;

    void Start()
    {
        cameraZoomOut = FindObjectsOfType<CameraZoomOut>();
        cinemachineVirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        StartOffSetY = transposer.m_FollowOffset.y;


    }

    void Update()
    {
        if (current)
        {
            if (zoomming)
            {
                if (transposer.m_FollowOffset.y < offSetY)
                {
                    transposer.m_FollowOffset.y += Time.deltaTime * 1.3f;
                    print("mais");


                }

            }
            else
            {
              
            }
        }
        else
        {
            if(zoom())
            {
                if (transposer.m_FollowOffset.y > StartOffSetY)
                {
                    transposer.m_FollowOffset.y -= Time.deltaTime;
                    print("menos");
                }
            }

        }

      
    }

    private bool zoom()
    {
        for (int i = 0; i < cameraZoomOut.Length; i++)
        {
            if (cameraZoomOut[i].current)
            {
                return false;
            }
          
        }
        return true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            current = true;

            cinemachineVirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

            zoomming = true;

            
        }
    }  
    private void OnTriggerStay(Collider other)
    {

    }  
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            current = false;
            zoomming = false;
        }
    }
}
