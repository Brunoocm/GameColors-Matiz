using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    [CreateAssetMenu(fileName = "New Link", menuName = "Editor/Link")]
    public class Link : ScriptableObject
    {
        public static Link current = null;

        [SerializeField] MainDoorsTransition level = null;

        public void GoTo()
        {
            current = this;
            level.Load();
        }
    }
}
