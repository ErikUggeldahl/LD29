using UnityEngine;
using System.Collections;

public class TeleportEffect : MonoBehaviour
{
    const float STROBE_TIME = 1;

    public void Play()
    {
        particleSystem.Play();

        StartCoroutine(LightPulse());
    }

    IEnumerator LightPulse()
    {
        float t = 1;

        while (t > 0)
        {
            t -= STROBE_TIME * Time.deltaTime;
            light.intensity = t;
            yield return null;
        }

        Destroy(gameObject);
    }
}
