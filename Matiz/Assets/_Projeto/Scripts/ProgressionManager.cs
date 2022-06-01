using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager instance;

    [Header("Arenas")]
    public bool desafio;
    public bool firstArena;
    public bool secondArena;

    [Header("Abilities")]
    public bool cinza;
    public bool vermelho;
    public bool azul;
    public bool verde;
    
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

    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
