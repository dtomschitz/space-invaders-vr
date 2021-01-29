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
/// <summary>
/// enum <c>DodgeDirection</c> contains the two options of directions in which the enemy can dodge.
/// </summary>
public enum DodgeDirection
{
    LEFT,
    RIGHT
}

/// <summary>
/// Class <c>Enemy</c> contains the logic of the enemies when they are shooting and when they are dodging.
/// </summary>
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


    public EnemyState State { get; protected set; }

    DodgeDirection dodgeDirection;
    NavMeshAgent agent;
    GameObject attackPoint;




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
        dodgeRange = Random.Range(dodgeRange-1, dodgeRange+3);
    }

    /// <summary>
    /// This method checks if the enemy is to close to the player if thats the case enemy gets destroyed.
    /// Additionally it will time the attacks of the enemy.
    /// It also times the dodges and the direction of the dodges.
    /// </summary>

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

    /// <summary>
    /// This method gets called if the Enemy gets killed.
    /// It will also call the <see cref="ShowExplosion"/> method.
    /// </summary>
    protected override void OnDeath()
    {
        base.OnDeath();
        GetComponent<SphereCollider>().enabled = false;
        ShowExplosion();
        
        Statistics.instance.AddKill();
        AudioManager.instance.PlaySound(Sound.Explosion, gameObject.transform.position);

        Destroy(gameObject);
    }

    /// <summary>
    /// This method gets called if the Enemy gets damaged.
    /// It will also call the <see cref="AddDamage"/> method.
    /// </summary>
    protected override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        Statistics.instance.AddDamage(damage);
    }
    /// <summary>
    /// This method gets called if the Enemy attacks.
    /// </summary>
    private void AttackPlayer()
    {
        EnemyProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.SetDamage(damage);
        timeBetweenAttacks = Random.Range(startTimeBtwAttacks, startTimeBtwAttacks+3);
    }
    /// <summary>
    /// This method gets called if the Enemy dodges.
    /// </summary>
    /// <param name="range">The Range in which the enemy dodges.</param>
    void Dodge(int range)
    {
        Vector3 destination = agent.transform.position;
        destination.x = destination.x + range;
        MoveToPosition(destination);
        
    }
    /// <summary>
    /// This method gets called whenever the enemy is moving to a postion.
    /// </summary>
    /// <param name="position">The postion to which the enemy moves.</param>
    void MoveToPosition(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position,position, Time.deltaTime * agent.speed);
        
    }
    /// <summary>
    /// This method gets called when the enemy dies.
    /// </summary>
    void ShowExplosion()
    {
        GameObject hit = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(hit, 3f);
    }
}
