using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager instance;

    [Header("Arenas")]
    public bool desafio;
    public bool unlockFirstArena;
    public bool unlockSecondArena;
    public bool unlockFinal;

    [Header("Abilities")]
    public bool cinza;
    public bool vermelho;
    public bool azul;
    public bool verde;
    
    [Header("Abilities")]
    public bool tutorial;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    
    }

    public void endTutorial()
    {
        tutorial = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
