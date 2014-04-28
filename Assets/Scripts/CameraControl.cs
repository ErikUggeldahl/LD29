using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    const float ZOOM_INC = 5f;
    const float DEFAULT_ZOOM_INC = 0.5f;

    [SerializeField]
    Transform toFollow;

    bool locked = true;

    float newZoom;

    void Start()
    {
        newZoom = camera.orthographicSize;
    }

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
        var toFollowPos = toFollow.position;
        var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        var newPos = (toFollowPos + mousePos) / 2f;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }

    public void IsLocked(bool locked)
    {
        this.locked = locked;
    }

    public void IncreaseZoom()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOut(ZOOM_INC));
    }

    public void IncreaseDefaultZoom()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOut(DEFAULT_ZOOM_INC));
    }

    IEnumerator ZoomOut(float increase)
    {
        newZoom += increase;

        while (camera.orthographicSize < newZoom)
        {
            camera.orthographicSize += Time.deltaTime;
            yield return null;
        }

        camera.orthographicSize = newZoom;
    }
}
