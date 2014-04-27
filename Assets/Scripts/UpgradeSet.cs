using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeSet : MonoBehaviour
{
    const int BAUBLE_COUNT = 8;
    const int POINTS_TO_LEVEL = 20;

    [SerializeField]
    CreatureControl creature;

    [SerializeField]
    Transform baubleRotator;
    [SerializeField]
    GameObject baubleObj;

    [SerializeField]
    GameObject levelUpText;
    [SerializeField]
    TextMesh sightUpText;
    [SerializeField]
    TextMesh moveUpText;

    float nextBaubleAngle = 0f;
    float baubleAngleIncrement = 360f / 8f;

    int pointTotal = 0;

    Transform[] baubles = new Transform[BAUBLE_COUNT];
    int baubleCount = 0;

    Queue<Upgrade> sightUpgrades;
    Queue<Upgrade> moveUpgrades;

    bool isChoosing = false;

    void Start()
    {
        AddBaubles();

        CreateUpgrades();
    }

    void Update()
    {
        if (isChoosing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && sightUpgrades.Count > 0)
            {
                sightUpgrades.Dequeue().Invoke();
                EndLevelUp();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && moveUpgrades.Count > 0)
            {
                moveUpgrades.Dequeue().Invoke();
                EndLevelUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
            AddPoints(POINTS_TO_LEVEL);
    }

    void CreateUpgrades()
    {
        sightUpgrades = new Queue<Upgrade>();
        sightUpgrades.Enqueue(new Upgrade("Increase Sight", creature.IncreaseSightRadius));
        sightUpgrades.Enqueue(new Upgrade("Enable Sonar", creature.EnableSonar));
        sightUpgrades.Enqueue(new Upgrade("Increase Light", creature.IncreaseLight));

        moveUpgrades = new Queue<Upgrade>();
        moveUpgrades.Enqueue(new Upgrade("Increase Speed", creature.IncreaseMoveSpeed));
        moveUpgrades.Enqueue(new Upgrade("Enable Teleport", creature.EnableTeleport));
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
        isChoosing = true;
        DisplayText();
    }

    void DisplayText()
    {
        levelUpText.SetActive(true);
        sightUpText.text = sightUpgrades.Count > 0 ? sightUpgrades.Peek().Name : string.Empty;
        moveUpText.text = moveUpgrades.Count > 0 ? moveUpgrades.Peek().Name : string.Empty;
    }

    void EndLevelUp()
    {
        levelUpText.SetActive(false);
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
