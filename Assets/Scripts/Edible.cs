using UnityEngine;
using System.Collections;

public class Edible : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;
    [SerializeField]
    Transform toMove;

    [SerializeField]
    int value = 1;

    float attractSpeed = 4f;
    float consumeDistance = 0.5f;

    void Start()
    {
        line.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        AdjustLinePosition(other.transform.position);
        line.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        line.enabled = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        AdjustLinePosition(other.transform.position);

        float distance = Vector3.Distance(transform.position, other.transform.position);

        if (distance < consumeDistance)
            Consume(other.gameObject.GetComponent<UpgradeSet>());
        else
            Attract(distance, other.transform.position);
        
    }

    void AdjustLinePosition(Vector3 end)
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, end);
    }

    void Consume(UpgradeSet upgrade)
    {
        upgrade.AddPoints(value);

        Destroy(toMove.gameObject);
    }

    void Attract(float distance, Vector3 to)
    {
        float attraction = attractSpeed * (1f / distance) * Time.deltaTime;
        toMove.position = Vector3.MoveTowards(toMove.position, to, attraction);

    }
}
