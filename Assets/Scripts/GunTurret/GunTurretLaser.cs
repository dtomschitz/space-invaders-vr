using UnityEngine;

public class GunTurretLaser : MonoBehaviour
{
    public float defaultLength = 15.0f;
    public string enemyTag;

    public GameObject hand;
    public GameObject dot;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RaycastHit hit = CreateForwardRaycast();
        Vector3 endPosition = transform.position + (hand.transform.forward * defaultLength);

        if (hit.collider)
        {
            if (hit.collider.tag == enemyTag)
            {
                Debug.Log("Hit " + hit.transform.tag);
            }

            endPosition = hit.point;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateForwardRaycast()
    {
        Ray ray = new Ray(transform.position, hand.transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, defaultLength);

        return hit;
    }
}



