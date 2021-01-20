using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunTurretLaser : MonoBehaviour
{
    public GunTurretPosition gunPosition;
    public float defaultLength = 40f;
    public GameObject dot;

    GameObject player;
    GameObject hand;
    XRRayInteractor rayInteractor;

    void Start()
    {
        player = Player.instance.gameObject;
        hand = GameObject.FindGameObjectWithTag(gunPosition == GunTurretPosition.LEFT ? "LeftHand" : "RightHand");
        rayInteractor = GameObject.FindGameObjectWithTag(gunPosition == GunTurretPosition.LEFT ? "LeftHandController" : "RightHandController").GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        rayInteractor.GetCurrentRaycastHit(out RaycastHit hit) ;
        Vector3 endPosition = hand.transform.position + (hand.transform.forward * defaultLength);

        if (hit.collider)
        {
            endPosition = hit.point;
        }

        dot.transform.position = endPosition;
        dot.transform.LookAt(player.transform);
    }
}



