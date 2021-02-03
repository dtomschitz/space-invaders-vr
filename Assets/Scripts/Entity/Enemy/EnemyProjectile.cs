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

    float defaultDestroyTime = 5f;

    void Start()
    {
        GameState.instance.OnGameStateChanged += OnGameStateChanged;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// This method lets the projecile move to the player.
    /// </summary>
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

        if (isSpaceship || isForceField)
        {
            if (hitPrefabs?.Length != 0) SpawnHitIndicator(collision);
            speed = 0;
            Destroy(gameObject);

            if (isSpaceship) Player.instance.Damage(damage);
            enemy.SendMessage("OnCancelAttack");
        }
    }

    void OnGameStateChanged(GameStateType newState)
    {
        canMove = newState == GameStateType.InGame;
        if (canMove) Invoke(nameof(DestroyProjectile), defaultDestroyTime);
        else CancelInvoke(nameof(DestroyProjectile));

        if (newState != GameStateType.InGame && projectileLoop != null)
        {
            projectileLoop.Stop();
        } else
        {
            projectileLoop.Play();
        }

        if (newState == GameStateType.GameOver)
        {
            if (projectileLoop != null) projectileLoop.Stop();
            Destroy(gameObject);
        }
    }

    void SpawnHitIndicator(Collision collision)
    {
        GameObject hitPrefab = hitPrefabs[Random.Range(0, hitPrefabs.Length)];
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        GameObject hit = Instantiate(hitPrefab, contact.point, rotation);

        AudioManager.instance.PlaySound(
            ForceField.instance.IsForceFieldEnabled ? Sound.ForceFieldImpact : Sound.EnemyProjectileHit, 
            collision.transform.position);

        Destroy(hit, 3f);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

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