using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControleSalas : MonoBehaviour
{
    [Header("GERAL")]
    public int levelAtual;
    public LayoutWave[] layouts;


    [Header("INIMIGOS")]
    public bool hasEnemy;
    public GameObject ranged;
    public GameObject melee;
    public GameObject explosive;

    [Header("PROGRESSÃO")]
    public Level[] levels;

    [Header("DEBUG")]
    public Level currentLevel;
    private LayoutWave currentLayout;
    public List<GameObject> inimigosDisponiveis;
    private int currentLevelNumber = 0;


    private void Awake()
    {
        StartCoroutine(StartGameplay());
    }

    private void SpawnEnemies()
    {
        inimigosDisponiveis.Clear();

        if (currentLevel.inimigos.melee) inimigosDisponiveis.Add(melee);
        if (currentLevel.inimigos.ranged) inimigosDisponiveis.Add(ranged);
        if (currentLevel.inimigos.explosive) inimigosDisponiveis.Add(explosive);

        foreach (Transform spawnpoint in currentLayout.spawnpoints)
        {
            Instantiate(inimigosDisponiveis[Random.Range(0, inimigosDisponiveis.Count)], spawnpoint.position, spawnpoint.rotation);
        }
    }

    IEnumerator StartGameplay()
    {
        currentLevelNumber++;
        currentLevel = levels[currentLevelNumber - 1];

        //if(currentLayout != null)currentLayout.visual.SetActive(false);

        currentLayout = layouts[currentLevelNumber - 1];
        //currentLayout.visual.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        SpawnEnemies();
    }
}

[System.Serializable]
public class Level
{
    public string name;
    public Inimigos inimigos;

}

[System.Serializable]
public class Inimigos
{
    public bool ranged;
    public bool melee;
    public bool explosive;
}