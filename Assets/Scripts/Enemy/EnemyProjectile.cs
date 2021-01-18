using UnityEngine;

public class EnemyProjectile : MonoBehaviour{

    public float speed;
    public GameObject[] hitPrefabs;

    float damage;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");       
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hitPrefabs?.Length != 0)
        {
            GameObject hitPrefab = hitPrefabs[Random.Range(0, hitPrefabs.Length)];
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            GameObject hit = Instantiate(hitPrefab, contact.point, rotation);
            Destroy(hit, 3f);
        }


        if (collision.gameObject.CompareTag("Spaceship"))
        {
            speed = 0;
            Player.instance.Damage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject, 5f);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}