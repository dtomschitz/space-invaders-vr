using UnityEngine;

/// <summary>
/// Class <c>EnemyProjectile</c> manages the enemy projectiles.
/// </summary>
public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public GameObject[] hitPrefabs;
    public AudioSource projectileLoop;

    bool canMove = true;
    bool isFired;
    float damage;

    GameObject enemy;
    GameObject player;

    void Start()
    {
        GameState.instance.OnGameStateChanged += OnGameStateChanged;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isFired && canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bool isSpaceship = collision.gameObject.CompareTag("Spaceship");
        bool isForceField = collision.gameObject.CompareTag("ForceField");

        // If the collided object is the spaceship or the force field, the hit
        // explosion should get instantiated. Incase the force field ist disabled
        // the player has to take damge.
        if (isSpaceship || isForceField)
        {
            if (hitPrefabs?.Length != 0) SpawnHitIndicator(collision);
            speed = 0;
            Destroy(gameObject);

            if (isSpaceship) Player.instance.Damage(damage);
            enemy.SendMessage("OnCancelAttack");
        }
    }

    /// <summary>
    /// This method can stop the projectile sound and movement based on the 
    /// current game state.
    /// </summary>
    void OnGameStateChanged(GameStateType newState)
    {
        canMove = newState == GameStateType.InGame;
        if (newState != GameStateType.InGame)
        {
            if (projectileLoop != null) projectileLoop.Stop();
        } else
        {
            if (projectileLoop != null) projectileLoop.Play();
        }

        if (newState == GameStateType.GameOver)
        {
            if (projectileLoop != null) projectileLoop.Stop();
            if (gameObject) Destroy(gameObject);
        }
    }
    	
    /// <summary>
    /// This method spawns the hit explosion and plays either the 
    /// <see cref="Sound.ForceFieldImpact"/> or the <see cref="Sound.EnemyProjectileHit"/>
    /// based on the current force field state.
    /// </summary>
    void SpawnHitIndicator(Collision collision)
    {
        GameObject hitPrefab = hitPrefabs[Random.Range(0, hitPrefabs.Length)];
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        GameObject hit = Instantiate(hitPrefab, contact.point, rotation);

        AudioManager.instance.PlaySound(
            ForceField.instance.IsForceFieldEnabled 
                ? Sound.ForceFieldImpact 
                : Sound.EnemyProjectileHit, 
            collision.transform.position);

        Destroy(hit, 3f);
    }

    /// <summary>
    /// This method sets the base damage of the projectile.
    /// </summary>
    /// <param name="damage">The amount of damage the projectile can deal.</param>
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetAttacker(GameObject enemy)
    {
        this.enemy = enemy;
    }

    public void Fire()
    {
        this.isFired = true;
    }
}