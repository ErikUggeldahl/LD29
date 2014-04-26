using UnityEngine;
using System.Collections;

public class CreatureControl : MonoBehaviour
{
    Camera mainCam;

    float moveSpeed = 60f;

    void Start()
    {
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition).ZMask();

        if (Input.GetKey(KeyCode.W))
        {
            Vector2 toMouse = (mousePos - transform.position).normalized;

            if (Vector2.Distance(mousePos, transform.position) > 0.6f)
                rigidbody2D.AddForce(toMouse * moveSpeed);

            transform.LookAt(mousePos);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = (Vector2)mousePos;

            if (!particleSystem.isPlaying)
                particleSystem.Play();
        }

        //Vector2 view = mainCam.ScreenToViewportPoint(Input.mousePosition);
        //if (view.x < 0.1)
        //    renderer.material.color = Color.red;
    }
}
