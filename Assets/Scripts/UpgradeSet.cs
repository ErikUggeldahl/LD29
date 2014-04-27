using UnityEngine;
using System.Collections;

public class UpgradeSet : MonoBehaviour
{
    const int BAUBLE_COUNT = 8;
    const int POINTS_TO_LEVEL = 9;

    [SerializeField]
    CreatureControl creature;

    [SerializeField]
    Transform baubleRotator;
    [SerializeField]
    GameObject baubleObj;

    float nextBaubleAngle = 0f;
    float baubleAngleIncrement = 360f / 8f;

    int pointTotal = 0;

    Transform[] baubles = new Transform[BAUBLE_COUNT];
    int baubleCount = 0;

    void Start()
    {
        AddBaubles();
    }
	
    void AddBaubles()
    {
        for (int i = 0; i < BAUBLE_COUNT; i++)
        {
            var newBauble = ((GameObject)Instantiate(baubleObj, transform.position, transform.rotation)).transform;
            newBauble.parent = baubleRotator;
            newBauble.Rotate(Vector3.left, nextBaubleAngle);
            nextBaubleAngle += baubleAngleIncrement;

            newBauble.gameObject.SetActive(false);

            baubles[i] = newBauble;
        }
    }

    public void AddPoints(int points)
    {
        pointTotal += points;
        if (pointTotal % POINTS_TO_LEVEL == 0)
            LevelUp();

        baubleCount += points;
        baubleCount %= (BAUBLE_COUNT + 1);
        EnableBaubles();
    }

    void LevelUp()
    {
        creature.IncreaseSightRadius();
        creature.IncreaseMoveSpeed();
    }

    void EnableBaubles()
    {
        for (int i = 0; i < BAUBLE_COUNT; i++)
        {
            if (i < baubleCount)
                baubles[i].gameObject.SetActive(true);
            else
                baubles[i].gameObject.SetActive(false);
        }
    }


}
