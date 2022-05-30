using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class AudioScript : MonoBehaviour
    {
        public static AudioScript Instance;

        [Header("Player")]
        [FMODUnity.EventRef] public string playerStepsEvent;
        [FMODUnity.EventRef] public string playerAttackEvent;
        [FMODUnity.EventRef] public string playerDamageEvent;
        [FMODUnity.EventRef] public string playerDeathEvent;

        [Header("Prism")]
        [FMODUnity.EventRef] public string prismDamageEvent;
        [FMODUnity.EventRef] public string prismCollectEvent;

        [Header("Colours")]
        [FMODUnity.EventRef] public string greyPassiveEvent;
        [FMODUnity.EventRef] public string greyDashEvent;
        [FMODUnity.EventRef] public string greySpecialEvent;

        [FMODUnity.EventRef] public string redPassiveEvent;
        [FMODUnity.EventRef] public string redDashEvent;
        [FMODUnity.EventRef] public string redSpecialEvent;

        [Header("NPCS")]
        [FMODUnity.EventRef] public string dialogEvent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
