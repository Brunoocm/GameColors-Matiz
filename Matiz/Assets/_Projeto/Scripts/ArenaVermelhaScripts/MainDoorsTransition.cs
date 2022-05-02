using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace OniricoStudios
{
    [CreateAssetMenu(fileName = "New Arena", menuName = "Editor/level")]
    public class MainDoorsTransition : ScriptableObject
    {
        public void Load()
        {
            SceneManager.LoadScene(name);
        }
    }
}
