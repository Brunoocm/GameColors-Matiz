using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace OniricoStudios
{
    public class ExtraLifeScript : MonoBehaviour
    {
        public int maxStacksLife;
        public int currentStacksLife;
        public GameObject extraObj;
        void Start()
        {
            extraObj.GetComponentInChildren<Slider>().maxValue = maxStacksLife;

        }

        void Update()
        {
            extraObj.GetComponentInChildren<Slider>().value = currentStacksLife;

            if (currentStacksLife >= maxStacksLife && CharacterStats.playerObj.health < 3)
            {
                CharacterStats.playerObj.health++;
                currentStacksLife = 0;
            }

            //if(Input.GetKeyDown(KeyCode.K))
            //{
            //    currentStacksLife++;
            //}
        }
    }
}
