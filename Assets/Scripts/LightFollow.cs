using UnityEngine;
using System.Collections;

public class LightFollow : MonoBehaviour
{
    [SerializeField]
    Transform toFollow;

	
    void Update()
    {
        var pos = transform.position;
        pos.x = toFollow.position.x;
        transform.position = pos;
    }
}
