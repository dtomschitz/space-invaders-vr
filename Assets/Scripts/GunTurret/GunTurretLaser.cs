using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunTurretLaser : MonoBehaviour
{
    public float defaultLength = 15.0f;
    public string enemyTag;

    public GameObject dot;
    public XRRayInteractor rayInteractor;

    GameObject player;
    GameObject hand;
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        player = Player.instance.gameObject;
        hand = gameObject.GetComponentInParent<GunTurret>().Hand;
    }

    void Update()
    {
        rayInteractor.GetCurrentRaycastHit(out RaycastHit hit) ;
        Vector3 endPosition = hand.transform.position + (hand.transform.forward * defaultLength);

        if (hit.collider)
        {
            endPosition = hit.point;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
        dot.transform.position = endPosition;

        dot.transform.LookAt(player.transform);
    }
}



