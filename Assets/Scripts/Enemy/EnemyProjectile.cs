using UnityEngine;

/// <summary>
/// Class <c>EnemyProjectile</c> manages the enemy projectiles.
/// </summary>

public class EnemyProjectile : MonoBehaviour{

    public float speed;
    public GameObject[] hitPrefabs;

    float damage;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 5f);
    }

    /// <summary>
    /// This method lets the projecile move to the player.
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
        }
    }

    void SpawnHitIndicator(Collision collision)
    {
        GameObject hitPrefab = hitPrefabs[Random.Range(0, hitPrefabs.Length)];
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        GameObject hit = Instantiate(hitPrefab, contact.point, rotation);

        AudioManager.instance.PlaySound(
            ForceField.instance.IsForceFieldEnabled ? Sound.ForceFieldImpact : Sound.Explosion, 
            collision.transform.position);

        Destroy(hit, 3f);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}