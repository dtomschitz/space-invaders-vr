using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public GameObject hitPrefab;

    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        if (hitPrefab)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(hitPrefab, position, rotation);
        }

        Debug.Log("Dadwd");

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Player.instance.combat.Attack(enemy.stats);
            }
        }

        Destroy(gameObject);
    }
}
