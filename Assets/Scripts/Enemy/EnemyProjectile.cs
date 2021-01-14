using UnityEngine;

public class EnemyProjectile : MonoBehaviour{

    public float speed;
    public float damage;

    private GameObject player;

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
        speed = 0;

        if (collision.gameObject.CompareTag("Player"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Player.instance.Attack(enemy);
            }
        }

        Destroy(gameObject);
    }
}