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

        Destroy(gameObject);
    }
}
