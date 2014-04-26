using UnityEngine;
using System.Collections;

public class FishSwarm : MonoBehaviour
{
    const int SPAWN_PER_GEN = 3;
    const int SPAWN_GENS = 3;

    const float WANDER_SPEED = 0.2f;
    const float PULL_DISTANCE = 4f;

    const float FEAR_DISTANCE = 8f;
    const float FEAR_ANGLE = 110f;
    const float FEAR_SPEED = 6f;
    const float FEAR_STOP_CHANCE = 0.01f;

    const float NEW_DIRECTION_CHANCE = 0.01f;

    [SerializeField]
    Transform toAvoid;

    int spawnGeneration = 0;
    bool isLeader;

    Transform leader;

    Quaternion wanderRotation;
    bool isScattering;

    void Start()
    {
        if (spawnGeneration < SPAWN_GENS)
            SpawnFollowers();

        isLeader = spawnGeneration == 0;

        transform.rotation = Random.rotation;
        wanderRotation = Random.rotation;
    }
    
    void Init(int spawnGeneration, Transform leader, Transform toAvoid)
    {
        this.spawnGeneration = spawnGeneration;
        this.leader = leader;
        this.toAvoid = toAvoid;

        name = leader.name + (spawnGeneration);
        transform.parent = leader.parent;
    }

    void SpawnFollowers()
    {
        for (int i = 0; i < SPAWN_PER_GEN; i++)
        {
            FishSwarm swarm = (Instantiate(gameObject) as GameObject).GetComponent<FishSwarm>();
            swarm.Init(spawnGeneration + 1, transform, toAvoid);
        }
    }
    
    void Update()
    {
        float toAvoidDist = Vector3.Distance(transform.position, toAvoid.position);
        float toAvoidAngle = Vector3.Angle(toAvoid.forward, transform.position - toAvoid.position);

        if (isScattering || toAvoidDist < FEAR_DISTANCE && toAvoidAngle < FEAR_ANGLE)
            Scatter();

        else if (isLeader)
        {
            if (toAvoidDist < FEAR_DISTANCE && toAvoidAngle < FEAR_ANGLE)
                Scatter();
            else
                Wander();
        }                                                                              
        else
        {
            float leaderDistance = Vector3.Distance(transform.position, leader.position);
            if (leaderDistance > PULL_DISTANCE)
                FollowLeader(leaderDistance);
            else
                Wander();
        }
    }

    void Wander()
    {
        transform.Translate(transform.forward.ZMask() * WANDER_SPEED * Time.deltaTime, Space.World);

        if (Random.value < NEW_DIRECTION_CHANCE)
            wanderRotation = Random.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, wanderRotation, Time.deltaTime);

    }

    void FollowLeader(float leaderDistance)
    {
        transform.LookAt(leader);
        transform.Translate(transform.forward.ZMask() * WANDER_SPEED * leaderDistance * Time.deltaTime, Space.World);
    }

    void Scatter()
    {
        Vector3 awayDir = transform.position - toAvoid.position;
        transform.LookAt(transform.position + awayDir);
        transform.Translate(transform.forward.ZMask() * FEAR_SPEED * Time.deltaTime, Space.World);

        isScattering = true;
        if (Random.value < FEAR_STOP_CHANCE)
            isScattering = false;
    }
}
