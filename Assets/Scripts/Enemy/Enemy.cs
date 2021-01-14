using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Attack,
    Strafe,
    Avoid,
    Seek
}

public enum DodgeDirection
{
    LEFT,
    RIGHT
}

public class Enemy : Entity
{
    public NavMeshAgent agent;
    public GameObject explosionPrefab;
    public EnemyProjectile projectilePrefab;

    [Header("Attack Settings")]
    public float timeBetweenAttacks;
    public float startTimeBtwAttacks;
    public float attackTime;

    [Header("Dodge Settings")]
    public int dodgeRange;
    public float dodgeStart;
    public float dodgeTime;

    private DodgeDirection dodgeDirection;

    public EnemyState State { get; protected set; }

    private GameObject player;

    protected override void Start()
    {
        base.Start();
        State = EnemyState.Seek;

        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = 1;
        agent.acceleration = 10;

        timeBetweenAttacks = startTimeBtwAttacks;
        dodgeDirection = (DodgeDirection) Random.Range(0, System.Enum.GetValues(typeof(DodgeDirection)).Length);
    }

    private void Update()
    {
        if (transform.position.z <= 0)
        {
            Destroy(agent.gameObject);
        }

        transform.LookAt(player.transform.position);

        if (timeBetweenAttacks <= 0)
        {
            AttackPlayer();
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }

        if (dodgeTime <= 0)
        {
            if (dodgeDirection == DodgeDirection.RIGHT)
            {
                Dodge(dodgeRange);
            }
            else
            {
                Dodge(-dodgeRange);
            }

            dodgeTime = dodgeStart;
        }
        else
        {
            dodgeTime -= Time.deltaTime;
            Vector3 destination = agent.transform.position;
            destination.z -= 1;
            MoveToPosition(destination);
        }

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        GetComponent<SphereCollider>().enabled = false;
        Shield.instance.AddShieldPower(10f);
        Statistics.instance.AddKill();

        ShowExplosion();

        Destroy(gameObject);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move

        EnemyProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.damage = damage;
        timeBetweenAttacks = startTimeBtwAttacks;
        ///attackcode here
    }

    private void Dodge(int range)
    {
        Vector3 destination = agent.transform.position;
        destination.x += range;
        MoveToPosition(destination);
        dodgeDirection = dodgeDirection == DodgeDirection.LEFT ? DodgeDirection.RIGHT : DodgeDirection.LEFT;
    }

    private void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
        if (agent.transform.position == position)
        {
            agent.speed = 0;
        }
    }

    private void ShowExplosion()
    {
        GameObject hit = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(hit, 3f);
    }
}
