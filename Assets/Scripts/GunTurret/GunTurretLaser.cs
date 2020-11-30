using UnityEngine;

public class GunTurretLaser : MonoBehaviour
{
    public float defaultLength = 15.0f;
    public string tag;

    public Material defaultMaterial;
    public Material highlightMaterial;

    // public GameObject dot;
    public GameObject hand;

    private LineRenderer lineRenderer;


    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {

        RaycastHit hit = CreateForwardRaycast();
        Vector3 endPosition = transform.position + (this.hand.transform.forward * defaultLength);

        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

       //  SetDotPosition(endPosition);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateForwardRaycast()
    {
        Ray ray = new Ray(transform.position, this.hand.transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, defaultLength);

        return hit;
    }

    private void SetDotPosition(Vector3 position)
    {
      //  dot.transform.position = position;

    }
}



