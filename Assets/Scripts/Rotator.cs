using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    float period = 2f;

    void Update()
    {
        transform.Rotate(Vector3.forward, 360f * Time.deltaTime / period);
    }
}
