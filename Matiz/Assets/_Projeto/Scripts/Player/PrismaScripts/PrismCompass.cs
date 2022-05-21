using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class PrismCompass : MonoBehaviour
    {
        public float offset;

        float angle;

        GameObject prism;

        private void Start()
        {
            prism = GameObject.FindGameObjectWithTag("Prism");
        }

        private void Update()
        {
            if(prism != null)
            {
                Vector3 difference = prism.transform.position - transform.position;
                angle = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(-90, 0, angle + offset);
            }
        }
    }
}
