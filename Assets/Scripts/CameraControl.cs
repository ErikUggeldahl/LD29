using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Transform toFollow;

    float followSpeed = 20f;

    void Update()
    {
        var camPos = camera.WorldToViewportPoint(transform.position);
        var toFollowPos = camera.WorldToViewportPoint(toFollow.position).ZMask();
        var distance = Vector3.Distance(camPos, toFollowPos);

        if (distance > 0.2)
        {
            var followMove = Vector3.MoveTowards(transform.position, toFollow.position, followSpeed * (1 + distance) * Time.deltaTime);
            followMove.z = transform.position.z;
            transform.position = followMove;
        }
    }
}
