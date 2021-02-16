using System.Collections;
using UnityEngine;

/// <summary>
/// Enum <c>DodgeDirection</c> contains the two options of directions in which 
/// the enemy can dodge.
/// </summary>
public enum DodgeDirection
{
    LEFT,
    RIGHT
}

/// <summary>
/// Class <c>Enemy</c> contains the logic of the enemies when they are shooting 
/// and when they are dodging.
/// </summary>
public class Enemy : Entity
{
    [Header("Prefabs")]
    public GameObject explosionPrefab;
    public EnemyProjectile projectilePrefab;
    public GameObject firePoint;

    // Damage Settings
    int minDamage;
    int maxDamage;

    // Doge Settings
    int minDodgeRange;
    int maxDodgeRange;
    float minDodgeSpeed;
    float maxDodgeSpeed;
    float minTimeBetweenDodges;
    float maxTimeBetweenDodges;
    float timeBetweenDodges;
    int dodgeRange;

    // Attack Settings
    float timeBetweenAttacks;
    float minStartTimeBetweenAttacks;
    float maxStartTimeBetweenAttacks;

    DodgeDirection dodgeDirection;
    GameObject attackPoint;
    Coroutine attackCoroutine;

    bool engagePlayer = false;
    bool attackRequested = false;

    protected override void Start()
    {
        base.Start();
        GameState.instance.OnGameStateChanged += OnGameStateChanged;

        GameObject[] attackPonints = GameObject.FindGameObjectsWithTag("AttackPoint");
        attackPoint = attackPonints[Random.Range(0, attackPonints.Length)];

        timeBetweenAttacks = Random.Range(minStartTimeBetweenAttacks, maxStartTimeBetweenAttacks);
        dodgeDirection = (DodgeDirection) Random.Range(0, System.Enum.GetValues(typeof(DodgeDirection)).Length);
    }

    /// <summary>
    /// This method checks if the enemy is to close to the player if thats the 
    /// case enemy gets destroyed. Additionally it will time the attacks of the 
    /// enemy. It also times the dodges and the direction of the dodges.
    /// </summary>
    void Update()
    {
        transform.LookAt(attackPoint.transform.position);

        if (CanAttack)
        {
            if (timeBetweenAttacks <= 0 && !engagePlayer && !attackRequested)
            {
                RequestAttack();
            }
            else
            {
                timeBetweenAttacks -= Time.deltaTime;
            }
        }

        if (timeBetweenDodges <= 0)
        {
            dodgeDirection = dodgeDirection == DodgeDirection.LEFT ? DodgeDirection.RIGHT : DodgeDirection.LEFT;
            timeBetweenDodges = Random.Range(minTimeBetweenDodges, maxTimeBetweenDodges);

            dodgeRange = Random.Range(minDodgeRange, maxDodgeRange);
        }
        else
        {
            timeBetweenDodges -= Time.deltaTime;
        }

        Dodge(dodgeDirection == DodgeDirection.RIGHT ? dodgeRange : -dodgeRange);
    }

    /// <summary>
    /// This method initializes the different config values of the specific
    /// enemy in order to keep those values dynamic based on the current wave.
    /// </summary>
    /// <param name="config">
    /// The specific <see cref="EnemyConfig"/> which should get loaded.
    /// </param>
    public void Init(EnemyConfig config)
    {
        maxHealth = Random.Range(config.minHealth, config.maxHealth);
        minDamage = config.minDamage;
        maxDamage = config.maxDamage;

        minDodgeRange = config.minDodgeRange;
        maxDodgeRange = config.maxDodgeRange;
        minDodgeSpeed = config.minDodgeSpeed;
        maxDodgeSpeed = config.maxDodgeSpeed;

        minTimeBetweenDodges = config.minTimeBetweenDodges;
        maxTimeBetweenDodges = config.maxTimeBetweenDodges;

        minStartTimeBetweenAttacks = config.minStartTimeBetweenAttacks;
        maxStartTimeBetweenAttacks = config.maxStartTimeBetweenAttacks;
    }

    /// <summary>
    /// This method is used for subscribing to the GameStateChange event in 
    /// order to enable or disable the attacking behaviour of the specific enemy.
    /// </summary>
    void OnGameStateChanged(GameStateType newState)
    {
        CanAttack = newState == GameStateType.InGame;
    }

    /// <summary>
    /// This method is used for requesting an attack on the player in order to 
    /// prevent multiple simultaneous shots.
    /// </summary>
    public void RequestAttack()
    {
        attackRequested = true;
        if (Player.instance.OnRequestAttack(gameObject.GetInstanceID()))
        {
            if (CanAttack) StartCoroutine(AttackCoroutine());
        } else
        {
            ResetTimeBetweenAttack();
            attackRequested = false;
        }
    }

    /// <summary>
    /// This method is used to cancel the currently ongoing attack so other 
    /// enemies can start their attack.
    /// </summary>
    public void OnCancelAttack()
    {
        Player.instance.OnCancelAttack(gameObject.GetInstanceID());
        ResetTimeBetweenAttack();
        engagePlayer = false;
        attackRequested = false;
    }

    /// <summary>
    /// This method gets called if the Enemy gets killed. It will also call 
    /// the <see cref="ShowExplosion"/> method.
    /// </summary>
    protected override void OnDeath()
    {
        base.OnDeath();
        GetComponent<SphereCollider>().enabled = false;
        ShowExplosion();
        
        Statistics.instance.AddKill();
        AudioManager.instance.PlaySound(Sound.Explosion, gameObject.transform.position);

        if (engagePlayer) Player.instance.OnCancelAttack(gameObject.GetInstanceID());

        Destroy(gameObject);
    }

    /// <summary>
    /// This method gets called if the Enemy attacks.
    /// It will also set a new random time between the attacks
    /// </summary>
    IEnumerator AttackCoroutine()
    {
        engagePlayer = true;

        AudioManager.instance.PlaySound(Sound.EnemyProjectileCharge, gameObject.transform.position);
        yield return new WaitForSeconds(1f);

        EnemyProjectile projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);
        AudioManager.instance.PlaySound(Sound.EnemyProjectileFire, gameObject.transform.position);
        projectile.SetAttacker(gameObject);
        projectile.SetDamage(Random.Range(minDamage, maxDamage));
        projectile.Fire();
    }

    /// <summary>
    /// This method gets called if the Enemy dodges.
    /// </summary>
    /// <param name="range">The Range in which the enemy dodges.</param>
    void Dodge(int range)
    {
        Vector3 destination = gameObject.transform.position;
        destination.x += range;
        MoveToPosition(destination);
    }

    /// <summary>
    /// This method gets called whenever the enemy is moving to a postion.
    /// </summary>
    /// <param name="position">The postion to which the enemy moves.</param>
    void MoveToPosition(Vector3 position)
    {
        float speed = Random.Range(minDodgeSpeed, maxDodgeSpeed);
        transform.position = Vector3.MoveTowards(transform.position,position, Time.deltaTime * speed);
    }

    /// <summary>
    /// This method gets called when the enemy dies.
    /// </summary>
    void ShowExplosion()
    {
        GameObject hit = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(hit, 3f);
    }

    /// <summary>
    /// This method resets the attack timer.
    /// </summary>
    void ResetTimeBetweenAttack()
    {
        timeBetweenAttacks = Random.Range(minStartTimeBetweenAttacks, maxStartTimeBetweenAttacks);
    }

    public bool CanAttack { set; get; } = true;
}
