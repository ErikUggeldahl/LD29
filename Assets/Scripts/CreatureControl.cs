using UnityEngine;
using System.Collections;

public class CreatureControl : MonoBehaviour
{
    [SerializeField]
    GameObject teleportEffectObj;

    Camera mainCam;

    float defaultDrag;
    float moveSpeed = 60f;

    void Start()
    {
        mainCam = Camera.main;
        defaultDrag = rigidbody2D.drag;
    }

    void FixedUpdate()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition).ZMask();

        BuoyancyControl();

        Move(mousePos);

        Teleport(mousePos);
    }

    void BuoyancyControl()
    {
        if (transform.position.y > 0f)
        {
            rigidbody2D.gravityScale = 1f;
            rigidbody2D.drag = 0f;
        }
        else
        {
            rigidbody2D.gravityScale = 0f;
            rigidbody2D.drag = defaultDrag;
        }
    }

    void Move(Vector3 to)
    {
        if (Input.GetKey(KeyCode.W) && transform.position.y <= 0f)
        {
            Vector2 toMouse = (to - transform.position).normalized;
            
            if (Vector2.Distance(to, transform.position) > 0.6f)
                rigidbody2D.AddForce(toMouse * moveSpeed);
            
            transform.LookAt(to);
        }
    }

    void Teleport(Vector3 to)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var teleportEffect = ((GameObject)Instantiate(teleportEffectObj, transform.position, Quaternion.identity)).GetComponent<TeleportEffect>();
            teleportEffect.Play();
            
            transform.position = to;
        }
    }
}
