using UnityEngine;
using System.Collections;

public class Edible : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;

    [SerializeField]
    Transform toMove;

    float attractSpeed = 4f;

    void Start()
    {
        line.enabled = false;
    }
	
    void Update()
    {
	
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

        if (distance < 0.5f)
            Consume();
        else
            Attract(distance, other.transform.position);
        
    }

    void AdjustLinePosition(Vector3 end)
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, end);
    }

    void Consume()
    {
        Destroy(toMove.gameObject);
    }

    void Attract(float distance, Vector3 to)
    {
        float attraction = attractSpeed * (1f / distance) * Time.deltaTime;
        toMove.position = Vector3.MoveTowards(toMove.position, to, attraction);

    }
}

