using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    CreatureHealth toDamage;
    public CreatureHealth ToDamage
    {
        get { return toDamage; }
    }

    [SerializeField]
    int damageGiven = 1;

    public delegate void DamageHandler();
    public event DamageHandler OnDamage;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        toDamage.DealDamage(damageGiven);

        if (OnDamage != null)
            OnDamage();
    }
}
