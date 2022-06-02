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
        [FMODUnity.EventRef] public string RedMeleeAtackEvent;
        [FMODUnity.EventRef] public string RedMeleeDamageEvent;
        [FMODUnity.EventRef] public string RedMeleeDeathEvent;

        [FMODUnity.EventRef] public string RedDashAtackEvent;
        [FMODUnity.EventRef] public string RedDashDamageEvent;
        [FMODUnity.EventRef] public string RedDashDeathEvent;

        [FMODUnity.EventRef] public string RedRangedAtackEvent;
        [FMODUnity.EventRef] public string RedRangedDamageEvent;
        [FMODUnity.EventRef] public string RedRangedDeathEvent;

        [FMODUnity.EventRef] public string RedExplosiveAtackEvent;
        [FMODUnity.EventRef] public string RedExplosiveDamageEvent;
        [FMODUnity.EventRef] public string RedExplosiveDeathEvent;

        [FMODUnity.EventRef] public string RedTankAtackEvent;
        [FMODUnity.EventRef] public string RedTankDamageEvent;
        [FMODUnity.EventRef] public string RedTankDeathEvent;

        [Header("NPCS")]
        [FMODUnity.EventRef] public string dialogEvent;

        [Header("Barco")]
        [FMODUnity.EventRef] public string BoatInEvent;
        [FMODUnity.EventRef] public string BoatOutEvent;

        [Header("UI")]
        [FMODUnity.EventRef] public string ButtonMouse;
        [FMODUnity.EventRef] public string ButtonPressed;
        [FMODUnity.EventRef] public string ButtonConfirm;
        [FMODUnity.EventRef] public string ButtonCancel;


        [Header("Save")]
        [FMODUnity.EventRef] public string IdleEvent;
        [FMODUnity.EventRef] public string OpenEvent;
        [FMODUnity.EventRef] public string CloseEvent;
        [FMODUnity.EventRef] public string SelectedGrayEvent;
        [FMODUnity.EventRef] public string SelectedRedEvent;
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

        public void PlaySound(string EventName)
        {
            FMODUnity.RuntimeManager.PlayOneShot(EventName, transform.position);
        }
    }
}
