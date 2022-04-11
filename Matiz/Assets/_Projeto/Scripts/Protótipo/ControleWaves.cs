using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleWaves : MonoBehaviour
{
    [Header( "Geral" )]
    public int wave;
    public GameObject[] layouts;

    private GameObject currentLayout;
    private int currentWave;

    private void Start()
    {
        currentWave = 0;
        NextWave( );
    }

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            NextWave( );
    }

    public void NextWave()
    {
        if ( currentLayout != null )
        {
            currentLayout.SetActive( false );
            currentWave++;
        }
        currentLayout = layouts[ currentWave ];
        currentLayout.SetActive( true );
        wave = currentWave;
    }

}
