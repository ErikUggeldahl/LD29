using UnityEngine;
using System.Collections;

public class CreatureHealth : MonoBehaviour
{
    int health = 10;

    public void DealDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Application.LoadLevel("Gameplay");

        transform.localScale = Vector3.one * health / 10f;
    }
}

