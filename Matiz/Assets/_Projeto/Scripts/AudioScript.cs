using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class AudioScript : MonoBehaviour
    {
        public static AudioScript Instance;


        //copiar a linha de baixo pra chamar o som 
        //FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance. =====  NOME DA VARI�VEL  =====, transform.position);

        [Header("Ambiente")]
        [FMODUnity.EventRef] public string PlantaEstourarEvent;
        [FMODUnity.EventRef] public string PlantaComerEvent;
        [FMODUnity.EventRef] public string FireEvent;

        [Header("Player")]
        [FMODUnity.EventRef] public string playerStepsEvent;
        [FMODUnity.EventRef] public string playerAttackEvent;
        [FMODUnity.EventRef] public string playerDamageEvent;
        [FMODUnity.EventRef] public string playerDeathEvent;

        [Header("Prism")]
        [FMODUnity.EventRef] public string prismIdleEvent;
        [FMODUnity.EventRef] public string prismDamageEvent;
        [FMODUnity.EventRef] public string prismCollectEvent;

        [Header("Colours")]
        [FMODUnity.EventRef] public string greyPassiveEvent;
        [FMODUnity.EventRef] public string greyDashEvent;
        [FMODUnity.EventRef] public string greySpecialEvent;

        [FMODUnity.EventRef] public string redPassiveEvent;
        [FMODUnity.EventRef] public string redDashChargeEvent;
        [FMODUnity.EventRef] public string redDashReleaseEvent;
        [FMODUnity.EventRef] public string redSpecialEvent;

        [Header("Armadilhas")]
        [FMODUnity.EventRef] public string SpearsEvent;
        [FMODUnity.EventRef] public string LavaEvent;
        [FMODUnity.EventRef] public string ArrowsEvent;

        [Header("Inimigos")]
        [FMODUnity.EventRef] public string RedMeleeEvent;
        [FMODUnity.EventRef] public string RedRangedEvent;
        [FMODUnity.EventRef] public string RedTankEvent;
        [FMODUnity.EventRef] public string RedExplosiveEvent;
        [FMODUnity.EventRef] public string RedDashEvent;

        [Header("NPCS")]
        [FMODUnity.EventRef] public string dialogEvent;

        [Header("Barco")]
        [FMODUnity.EventRef] public string BoatInOutEvent;

        [Header("Save")]
        [FMODUnity.EventRef] public string IdleEvent;
        [FMODUnity.EventRef] public string OpenCloseEvent;
        [FMODUnity.EventRef] public string ColourSelectedEvent;
        [FMODUnity.EventRef] public string SelectingEvent;

        [Header("Menu")]
        [FMODUnity.EventRef] public string MenuAmbientEvent;

        [Header("Musicas")]
        [FMODUnity.EventRef] public string TemaPrincipalEvent;
        [FMODUnity.EventRef] public string TemaColiseuEvent;
        [FMODUnity.EventRef] public string TemaBrigaEvent;

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
