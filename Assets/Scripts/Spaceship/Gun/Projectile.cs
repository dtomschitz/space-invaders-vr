using UnityEngine;

/// <summary>
/// Class <c>Projectile</c> is used in order to handle the projectile logic, 
/// spawn the effects and sounds if the player hit an enemy.
/// </summary>
public class Projectile : MonoBehaviour
{
    public float speed;
    public GameObject[] hitPrefabs;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            speed = 0;

            if (hitPrefabs?.Length != 0)
            {   
                // Determine a one of the hit prefab and instantiate it at the
                // contact point.
                GameObject hitPrefab = hitPrefabs[Random.Range(0, hitPrefabs.Length)];
                ContactPoint contact = collision.contacts[0];
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                GameObject hit = Instantiate(hitPrefab, contact.point, rotation);
                Destroy(hit, 3f);
            }

            // If the projectile collided with an enemy, the Attack method gets
            // called in order to update the health and combat stats of the 
            // respective enemy.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null) Player.instance.Attack(enemy);

            Destroy(gameObject);
        }
    }
}
