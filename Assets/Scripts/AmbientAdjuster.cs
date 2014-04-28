using UnityEngine;
using System.Collections;

public class AmbientAdjuster : MonoBehaviour
{
    [SerializeField]
    Color fullAmbient;
    [SerializeField]
    Color zeroAmbient;

    [SerializeField]
    float yMax;
    [SerializeField]
    float yMin;

    float range;

    void Start()
    {
        range = yMax - yMin;
    }

    void Update()
    {
        float t = (transform.position.y - yMin) / range;
        Color ambient = Color.Lerp(zeroAmbient, fullAmbient, t);
        RenderSettings.ambientLight = ambient;
    }
}
