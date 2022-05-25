using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class CameraScript : MonoBehaviour
    {
        float shakeTime;
        Vector3 orgPos;

        public void Shake(float magnitude, float time)
        {
            shakeTime = time;
            orgPos = transform.localPosition;

            do
            {
                float offset = Random.Range(-1.0f, 1.0f) * magnitude;
                orgPos.x += offset;
                transform.localPosition = orgPos;
                shakeTime -= Time.deltaTime;

            } while (shakeTime > 0);

            transform.localPosition = orgPos;
        }
    }
}
