using UnityEngine;
using System.Collections;

public class CreatureControl : MonoBehaviour
{
    const float START_MOVE_SPEED = 80f;
    const float MOVE_SPEED_INC = 30f;

    [SerializeField]
    GameObject teleportEffectObj;

    [SerializeField]
    Light creatureLight;

    [SerializeField]
    TextMesh teleportText;
    [SerializeField]
    TextMesh sonarText;

    Camera mainCam;
    CameraControl cameraControl;

    float defaultDrag;
    float moveSpeed = START_MOVE_SPEED;

    KeyCode moveKey = KeyCode.W;
    KeyCode teleportKey = KeyCode.Space;

    bool isBelowWater = true;
    bool canTeleport = false;
    bool canSonar = false;
    public bool CanSonar
    {
        get { return canSonar; }
    }

    float teleportTotalCooldown = 3f;
    float teleportCooldown = 0f;

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
        if (teleportCooldown > 0f)
            teleportCooldown -= Time.deltaTime;

        if (canTeleport && Input.GetKeyDown(teleportKey))
        {
            var teleportEffect = ((GameObject)Instantiate(teleportEffectObj, transform.position, Quaternion.identity)).GetComponent<TeleportEffect>();
            teleportEffect.Play();
            
            transform.position = to;

            teleportCooldown = teleportTotalCooldown;
        }
    }

    public void IncreaseDefaultZoom()
    {
        cameraControl.IncreaseDefaultZoom();
    }

    public void IncreaseSightRadius()
    {
        cameraControl.IncreaseZoom();
    }

    public void IncreaseLight()
    {
        creatureLight.range += 30f;
        creatureLight.intensity += 2f;
    }

    public void IncreaseMoveSpeed()
    {
        moveSpeed += MOVE_SPEED_INC;
    }

    public void EnableTeleport()
    {
        canTeleport = true;

        teleportText.gameObject.SetActive(true);
        teleportText.transform.position = transform.position;
    }

    public void EnableSonar()
    {
        canSonar = true;

        sonarText.gameObject.SetActive(true);
        sonarText.transform.position = transform.position;
    }
}
