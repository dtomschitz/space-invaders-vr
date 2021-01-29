using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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
    [Header("Prefabs")]
    public GameObject explosionPrefab;
    public EnemyProjectile projectilePrefab;

    [Header("Attack Settings")]
    public float timeBetweenAttacks;
    public float startTimeBtwAttacks;
    public float attackTime;
    public bool disableAttack;

    [Header("Dodge Settings")]
    public int dodgeRange;
    public float dodgeStart;
    public float dodgeTime;
    public GameObject[] waypoints;
    int current = 0;
    float WPradius = 1;


    public EnemyState State { get; protected set; }

    DodgeDirection dodgeDirection;
    NavMeshAgent agent;
    GameObject attackPoint;
    public float speed;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        State = EnemyState.Seek;

        GameObject[] attackPonints = GameObject.FindGameObjectsWithTag("AttackPoint");
        attackPoint = attackPonints[Random.Range(0, attackPonints.Length)];

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

        transform.LookAt(attackPoint.transform.position);

        if (!disableAttack)
        {
            if (timeBetweenAttacks <= 0)
            {
                AttackPlayer();
            }
            else
            {
                timeBetweenAttacks -= Time.deltaTime;
            }
        }

        if (dodgeTime <= 0)
        {
            
          
            dodgeDirection = dodgeDirection == DodgeDirection.LEFT ? DodgeDirection.RIGHT : DodgeDirection.LEFT;
            dodgeTime = dodgeStart;
        }
        else
        {
            dodgeTime -= Time.deltaTime;
            
        }

        if (dodgeDirection == DodgeDirection.RIGHT)
        {
            Dodge(dodgeRange);
        }
        else
        {
            Dodge(-dodgeRange);
        }

        

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        GetComponent<SphereCollider>().enabled = false;
        ShowExplosion();
        Statistics.instance.AddKill();

        Destroy(gameObject);
    }

    protected override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        Statistics.instance.AddDamage(damage);
    }

    private void AttackPlayer()
    {
        EnemyProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.SetDamage(damage);
        timeBetweenAttacks = startTimeBtwAttacks;
    }

    void Dodge(int range)
    {
        Vector3 destination = agent.transform.position;
        destination.x = destination.x + range;
        MoveToPosition(destination);
        
    }

    void MoveToPosition(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position,position, Time.deltaTime * agent.speed);
        
    }

    void ShowExplosion()
    {
        GameObject hit = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(hit, 3f);
    }
}
