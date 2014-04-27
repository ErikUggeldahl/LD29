using UnityEngine;
using System.Collections;

public class CreatureControl : MonoBehaviour
{
    const float START_MOVE_SPEED = 60f;
    const float MOVE_SPEED_INC = 20f;

    [SerializeField]
    GameObject teleportEffectObj;

    Camera mainCam;
    CameraControl cameraControl;

    float defaultDrag;
    float startMoveSpeed = START_MOVE_SPEED;
    float moveSpeed = START_MOVE_SPEED;

    KeyCode moveKey = KeyCode.W;
    KeyCode teleportKey = KeyCode.Space;

    bool isBelowWater = true;

    void Start()
    {
        mainCam = Camera.main;
        cameraControl = mainCam.GetComponent<CameraControl>();
        defaultDrag = rigidbody2D.drag;

        AddStartRotation();
    }

    void AddStartRotation()
    {
        rigidbody2D.AddTorque(10000f);
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
        if (transform.position.y > 0f && isBelowWater)
        {
            isBelowWater = false;
            rigidbody2D.gravityScale = 1f;
            rigidbody2D.drag = 0f;
            cameraControl.IsLocked(true);
        }
        else if (transform.position.y <= 0f && !isBelowWater)
        {
            isBelowWater = true;
            rigidbody2D.gravityScale = 0f;
            rigidbody2D.drag = defaultDrag;
            cameraControl.IsLocked(false);
        }
    }

    void Move(Vector3 to)
    {
        if (Input.GetKey(moveKey) && transform.position.y <= 0f)
        {
            Vector2 toMouse = (to - transform.position).normalized;
            
            if (Vector2.Distance(to, transform.position) > 0.6f)
                rigidbody2D.AddForce(toMouse * moveSpeed);
            
            transform.LookAt(to);
        }
    }

    void Teleport(Vector3 to)
    {
        if (Input.GetKeyDown(teleportKey))
        {
            var teleportEffect = ((GameObject)Instantiate(teleportEffectObj, transform.position, Quaternion.identity)).GetComponent<TeleportEffect>();
            teleportEffect.Play();
            
            transform.position = to;
        }
    }

    public void IncreaseSightRadius()
    {
        cameraControl.IncreaseZoom();
    }

    public void IncreaseMoveSpeed()
    {
        moveSpeed += MOVE_SPEED_INC;
    }
}
