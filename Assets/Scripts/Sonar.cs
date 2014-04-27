using UnityEngine;
using System.Collections;

public class Sonar : MonoBehaviour
{
    [SerializeField]
    Shader sonarShader;

    [SerializeField]
    Transform sonarRing;

    Camera mainCam;

    bool isPinging = false;
    float maxDistance = 50f;
    float startSpeed = 5f;
    float maxSpeed = 20f;
    float accel = 3f;

    KeyCode pingKey = KeyCode.F;

    void Start()
    {
        mainCam = Camera.main;
    }
	
    void Update()
    {
        if (Input.GetKeyDown(pingKey))
            StartPing();
    }

    void StartPing()
    {
        if (isPinging)
            return;

        StartCoroutine(SonarPing());
    }

    IEnumerator SonarPing()
    {
        mainCam.SetReplacementShader(sonarShader, "RenderType");
        float originalOrtho = mainCam.orthographicSize;

        Vector3 pos = transform.position;
        Shader.SetGlobalVector("_SonarOrigin", new Vector4(pos.x, pos.y, pos.z, 0f));
        Shader.SetGlobalFloat("_SonarMaxDistance", maxDistance);

        sonarRing.gameObject.SetActive(true);

        isPinging = true;
        float distance = 0;
        float speed = startSpeed;

        while (distance < maxDistance)
        {
            if (Input.GetKeyUp(pingKey))
                break;

            if (speed < maxSpeed)
                speed += accel * Time.deltaTime;

            distance += speed * Time.deltaTime;
            Shader.SetGlobalFloat("_SonarDistance", distance);
            mainCam.orthographicSize = distance + 1f;

            sonarRing.localScale = new Vector3(distance, distance, distance);

            yield return null;
        }

        mainCam.ResetReplacementShader();
        mainCam.orthographicSize = originalOrtho;

        sonarRing.gameObject.SetActive(false);

        isPinging = false;
    }
}
