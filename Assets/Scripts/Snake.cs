using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour
{
    const float WANDER_RADIUS = 20f;

    const float ATTACK_DISTANCE = 7f;
    const float ATTACK_ANGLE = 45f;
    const float FOLLOW_DISTANCE = 20f;

    const float WANDER_SPEED = 0.75f;
    const float ATTACK_SPEED = 4f;
    
    [SerializeField]
    DamageDealer damageDealer;

    [SerializeField]
    Animation snakeAnimation;

    [SerializeField]
    AnimationClip attackAnimation;

    [SerializeField]
    AudioClip wanderSound;
    [SerializeField]
    AudioClip attackSound;

    Transform toAttack;

    bool attacking = false;

    Vector3 wanderTo;

    void Start()
    {
        toAttack = damageDealer.ToDamage.transform;
        damageDealer.OnDamage += AttackAnim;

        wanderTo = CreateWanderTo();
    }
	
    void Update()
    {
        float toAttackDist = Vector3.Distance(transform.position, toAttack.position);
        float toAttackAngle = Vector3.Angle(toAttack.forward, transform.position - toAttack.position);

        if (!attacking && toAttackDist < ATTACK_DISTANCE && toAttackAngle < ATTACK_ANGLE)
            Attack(toAttackDist);
        else if (attacking & toAttackDist < FOLLOW_DISTANCE)
            Attack(toAttackDist);
        else
            Wander();
    }
    
    void Attack(float dist)
    {
        if (attacking = false)
        {
            audio.clip = wanderSound;
            audio.Play();
        }

        attacking = true;

        if (dist > 0.4f)
            transform.LookAt(toAttack);

        var dir = (toAttack.position - transform.position).normalized;
        transform.position += dir * ATTACK_SPEED * Time.deltaTime;
    }

    void Wander()
    {
        if (attacking)
            wanderTo = CreateWanderTo();
        attacking = false;

        var dir = (wanderTo - transform.position).normalized;
        transform.position += dir * WANDER_SPEED * Time.deltaTime;
        transform.LookAt(wanderTo);

        if (Vector3.Distance(wanderTo, transform.position) < 0.5f)
            wanderTo = CreateWanderTo();
    }

    Vector3 CreateWanderTo()
    {
        Vector3 to = (Random.insideUnitSphere.ZMask() * WANDER_RADIUS) + transform.position;
        if (to.y > 0)
            to.y = 0;
        return to;
    }

    void AttackAnim()
    {
        snakeAnimation.Play("Snake_Attack");
        snakeAnimation.PlayQueued("Snake_Move");

        audio.clip = attackSound;
        audio.Play();
    }
}
