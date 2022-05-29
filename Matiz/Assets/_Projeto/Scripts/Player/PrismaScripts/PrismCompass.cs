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

                compassArrow.transform.localScale = (new Vector3(0.4f, 0.4f, 0.4f) / arrowSize) * 2.3f;

                //if (arrowSize > 0.5f && arrowSize < 1.5f)
                //{

                //}
                //if (arrowSize >= 1.5f)
                //{
                //    compassArrow.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                //}
                //else if (arrowSize <= 0.5f)
                //{
                //    compassArrow.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                //}

            }
            else
            {
                prism = GameObject.FindGameObjectWithTag("Prism");
            }
        }
    }
}
