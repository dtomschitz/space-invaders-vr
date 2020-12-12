using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunTurretLaser : MonoBehaviour
{
    public float defaultLength = 15.0f;
    public string enemyTag;

    public GameObject hand;
    public GameObject dot;
    public XRRayInteractor rayInteractor;

    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        rayInteractor.GetCurrentRaycastHit(out RaycastHit hit) ;
        Vector3 endPosition = hand.transform.position + (hand.transform.forward * defaultLength);

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
        dot.transform.position = endPosition;
    }
}



