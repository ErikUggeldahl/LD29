using UnityEngine;
using System.Collections;

public class CreatureHealth : MonoBehaviour
{
    int health = 10;

    void Start()
    {
	
    }
	
    void Update()
    {
	
    }

    public void DealDamage(int damage)
    {
        health -= damage;
    }
}

