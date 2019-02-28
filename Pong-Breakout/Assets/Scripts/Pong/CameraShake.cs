using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    public static CameraShake instance;

    private void Awake()
    {
        originalPos = transform.localPosition;

        instance = this;
    }

    public static void Shake(float duration, float amount)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.ShakeCamera(duration, amount));
    }

    public IEnumerator ShakeCamera(float duration, float amount)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            transform.position = originalPos + Random.insideUnitSphere * amount;

            duration -= Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }


}
