using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunLaser : MonoBehaviour
{
    public GunPosition gunPosition;
    public float defaultLength = 40f;
    public GameObject dot;
    public LayerMask ignoreLayer;

    GameObject player;
    GameObject hand;
    XRRayInteractor rayInteractor;

    bool isEnabled = false;

    void Start()
    {
        player = Player.instance.gameObject;
        hand = GameObject.FindGameObjectWithTag(gunPosition == GunPosition.LEFT ? "LeftHand" : "RightHand");
        rayInteractor = GameObject.FindGameObjectWithTag(gunPosition == GunPosition.LEFT ? "LeftHandController" : "RightHandController").GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        if (IsEnabled)
        {
            rayInteractor.GetCurrentRaycastHit(out RaycastHit hit);
            Vector3 endPosition = hand.transform.position + (hand.transform.forward * defaultLength);

            if (hit.collider)
            {
                endPosition = hit.point;
            }

            dot.transform.position = endPosition;
            dot.transform.LookAt(player.transform);
        }
    }

    public bool IsEnabled
    {
        set
        {
            isEnabled = value;
            dot.SetActive(value);
        }

        get => isEnabled;
    }
}



