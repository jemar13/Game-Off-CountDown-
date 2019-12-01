using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
 public  IEnumerator Shake(float duration, float mag)
    {
        Vector3 initialPos = transform.localPosition;
        float passBy = 0.0f;
        while (passBy<duration)
        {
            float x = Random.Range(-1f, 1f) * mag;
            float y = Random.Range(-1f, 1f) * mag;
            transform.localPosition = new Vector3(x, y, initialPos.z);
            passBy += Time.deltaTime; 
            yield return null; 
        }

        transform.localPosition = initialPos;
    }
}
