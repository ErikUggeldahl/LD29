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

    [SerializeField]
    float cooldown = 1f;

    float currentCooldown = 0f;

    public delegate void DamageHandler();
    public event DamageHandler OnDamage;

    void Update()
    {
        if (currentCooldown > 0f)
            currentCooldown -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentCooldown > 0f)
            return;

        toDamage.DealDamage(damageGiven);
        currentCooldown += cooldown;

        if (OnDamage != null)
            OnDamage();
    }
}
