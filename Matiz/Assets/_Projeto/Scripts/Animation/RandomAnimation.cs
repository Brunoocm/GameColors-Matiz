using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class RandomAnimation : MonoBehaviour
    {
        public string trigger;
        public float min, max;

        Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();

            anim.speed = Random.Range(min, max);

            if(trigger != null)
            {
                //anim.SetTrigger(trigger);
            }
        }
    }
}
