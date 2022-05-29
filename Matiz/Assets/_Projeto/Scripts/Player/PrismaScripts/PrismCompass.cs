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
        GameObject compassArrow;

        private void Start()
        {
            prism = GameObject.FindGameObjectWithTag("Prism");
            compassArrow = transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            if(prism != null)
            {
                Vector3 difference = prism.transform.position - transform.position;
                angle = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(-90, 0, angle + offset);

                float arrowSize = Vector3.Distance(prism.transform.position, transform.position) / 10;

                //compassArrow.transform.localScale = new Vector3(arrowSize, arrowSize, arrowSize);

                //compassArrow.transform.localScale = (new Vector3(0.4f, 0.4f, 0.4f) / arrowSize) * 2.3f;

                if (arrowSize > 0.6f && arrowSize < 2)
                {
                    compassArrow.transform.localScale = (new Vector3(0.4f, 0.4f, 0.4f) / arrowSize) * 2.3f;
                }
                else if(arrowSize <= 0.6f)
                {
                    compassArrow.transform.localScale = (new Vector3(0.4f, 0.4f, 0.4f) / 0.6f) * 2.3f;
                }
                else if(arrowSize >= 2)
                {
                    compassArrow.transform.localScale = (new Vector3(0.4f, 0.4f, 0.4f) / 2) * 2.3f;
                }
            }
            else
            {
                prism = GameObject.FindGameObjectWithTag("Prism");
            }
        }
    }
}
