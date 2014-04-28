using UnityEngine;
using System.Collections;

public class FishSwarm : MonoBehaviour
{
    const int SPAWN_PER_GEN = 3;
    const int SPAWN_GENS = 3;
    const float SPAWN_RADIUS = 5f;

    const float WANDER_SPEED = 0.2f;
    const float PULL_DISTANCE = 4f;

    const float FEAR_DISTANCE = 4f;
    const float FEAR_ANGLE = 200f;
    const float FEAR_SPEED = 6f;
    const float ATTRACT_SPEED = 2f;
    const float FEAR_STOP_CHANCE = 0.01f;
    const float FEAR_SOUND_CHANCE = 0.01f;

    const float NEW_DIRECTION_CHANCE = 0.01f;

    [SerializeField]
    Transform toAvoid;

    [SerializeField]
    Edible edible;

    [SerializeField]
    AudioClip[] sounds;

    int spawnGeneration = 0;
    bool isLeader;

    Transform leader;

    Quaternion wanderRotation;
    bool isScattering;
    float scale;

    void Start()
    {
        if (spawnGeneration < SPAWN_GENS)
            SpawnFollowers();

        isLeader = spawnGeneration == 0;

        transform.rotation = Random.rotation;
        wanderRotation = Random.rotation;
        scale = transform.localScale.x;
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
            var spawnLocation = transform.position + Random.insideUnitSphere.ZMask() * SPAWN_RADIUS * scale;
            FishSwarm swarm = (Instantiate(gameObject, spawnLocation, transform.rotation) as GameObject).GetComponent<FishSwarm>();
            swarm.transform.position = spawnLocation;
            swarm.Init(spawnGeneration + 1, transform, toAvoid);
        }
    }
    
    void Update()
    {
        float toAvoidDist = Vector3.Distance(transform.position, toAvoid.position);
        float toAvoidAngle = Vector3.Angle(toAvoid.forward, toAvoid.position - transform.position);

        if (isScattering || toAvoidDist < FEAR_DISTANCE && toAvoidAngle < FEAR_ANGLE)
            Scatter();

        else if (isLeader)
            Wander();

        else
        {
            if (leader == null)
                Wander();
            else
            {
                float leaderDistance = Vector3.Distance(transform.position, leader.position);
                if (leaderDistance > PULL_DISTANCE * scale)
                    FollowLeader(leaderDistance);
                else
                    Wander();
            }
        }

        LimitY();
    }

    void Wander()
    {
        transform.Translate(transform.forward.ZMask() * WANDER_SPEED * scale * Time.deltaTime, Space.World);

        if (Random.value < NEW_DIRECTION_CHANCE)
            wanderRotation = Random.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, wanderRotation, Time.deltaTime);

    }

    void FollowLeader(float leaderDistance)
    {
        transform.LookAt(leader);
        transform.Translate(transform.forward.ZMask() * WANDER_SPEED * scale * leaderDistance * Time.deltaTime, Space.World);
    }

    void Scatter()
    {
        float speed = edible.IsAttracted ? FEAR_SPEED : ATTRACT_SPEED;

        Vector3 awayDir = transform.position - toAvoid.position;
        transform.LookAt(transform.position + awayDir);
        transform.Translate(transform.forward.ZMask() * speed * scale * Time.deltaTime, Space.World);

        PlayScatterSound();

        isScattering = true;
        if (Random.value < FEAR_STOP_CHANCE)
            isScattering = false;
    }

    void PlayScatterSound()
    {
        if (Random.value <= FEAR_SOUND_CHANCE)
        {
            var rand = Random.Range(0, sounds.Length);
            audio.clip = sounds[rand];
            audio.Play();
        }
    }

    void LimitY()
    {
        if (transform.position.y > 0)
        {
            var pos = transform.position;
            pos.y = 0;
            transform.position = pos;
        }
    }
}
