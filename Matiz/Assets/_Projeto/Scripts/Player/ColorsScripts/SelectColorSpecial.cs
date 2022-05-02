using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class SelectColorSpecial : MonoBehaviour
    {
        public ColorSpecial[] colors;
        public int index;

        MainCheckpoint mainCheckpoint => gameObject.GetComponentInParent<MainCheckpoint>();
        CheckpointScript checkpointScript => gameObject.GetComponentInParent<CheckpointScript>();
        void Start()
        {

        }

        void Update()
        {
            //int previousSelected = index;

            colors[index].isSelected = true;

            if (index != 0)
            {
                colors[index - 1].isSelected = false;
            }
            if (index != colors.Length - 1)
            {
                colors[index + 1].isSelected = false;
            }

            if (checkpointScript.selectColor)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (index > 0)
                    {
                        index--;

                    }
                    //esquerda
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (index < colors.Length - 1)
                    {
                        index++;

                    }
                    //Direita
                }
            }



        }
    }
}