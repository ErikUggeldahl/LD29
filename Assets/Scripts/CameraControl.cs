using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Transform toFollow;

    float followSpeed = 80f;

    bool locked = true;

    void Update()
    {
        if (locked)
            HardFollow();
        else
            SpringFollow();
    }

    void HardFollow()
    {
        var followPos = toFollow.position;
        followPos.z = transform.position.z;
        transform.position = followPos;
    }

    void SpringFollow()
    {
        var camPos = camera.WorldToViewportPoint(transform.position);
        var toFollowPos = camera.WorldToViewportPoint(toFollow.position).ZMask();
        var distance = Vector3.Distance(camPos, toFollowPos);
        
        if (distance > 0.1)
        {
            var followMove = Vector3.MoveTowards(transform.position, toFollow.position, followSpeed * (1 + distance) * Time.deltaTime);
            followMove.z = transform.position.z;
            transform.position = followMove;
        }
    }

    public void IsLocked(bool locked)
    {
        this.locked = locked;
    }
}
