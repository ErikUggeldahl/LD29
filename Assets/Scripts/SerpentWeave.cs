using UnityEngine;
using System.Collections;

public class SerpentWeave : MonoBehaviour
{
    const float MOTOR_SPEED = 20f;
    const float INTERVAL = 10f;

    [SerializeField]
    HingeJoint2D[] parts;

    void Start()
    {
        StartCoroutine(ChangeDirections());
    }
	
    IEnumerator ChangeDirections()
    {
        int positive = 0;
        while (true)
        {
            for (int i = 0; i < parts.Length; i++)
                ChangePart(parts[i], i % 2 == positive);

            positive++;
            positive %= 2;

            yield return new WaitForSeconds(INTERVAL);
        }
    }

    void ChangePart(HingeJoint2D part, bool positive)
    {
        float min = positive ? 0f : -MOTOR_SPEED;
        float max = positive ? MOTOR_SPEED : 0f;

        var motor = part.motor;
        motor.motorSpeed = Random.Range(min, max);
        part.motor = motor;
    }
}
