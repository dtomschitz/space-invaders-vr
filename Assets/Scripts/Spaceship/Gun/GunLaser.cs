using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Class <c>GunLaser</c> is used in order to find the correct direction the 
/// player is aiming to. The calculated point is then used to update the position
/// and rotation of the specific gun.
/// </summary>
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
        hand = GameObject.FindGameObjectWithTag(gunPosition == GunPosition.LEFT 
            ? "LeftHand" 
            : "RightHand");
        rayInteractor = GameObject.FindGameObjectWithTag(
            gunPosition == GunPosition.LEFT 
                ? "LeftHandController" 
                : "RightHandController").GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        if (IsEnabled)
        {   
            rayInteractor.GetCurrentRaycastHit(out RaycastHit hit);
            Vector3 endPosition = hand.transform.position + (hand.transform.forward * defaultLength);

            // Override the position if the raycast collided in order to render 
            // the crosshair before the respective object.
            if (hit.collider)
            {
                endPosition = hit.point;
            }

            // Update the position of the end point of the raycast and rotate it 
            // towards the players direction.
            dot.transform.position = endPosition;
            dot.transform.LookAt(player.transform);
        }
    }

    /// <summary>
    /// This method checks if the specific "laser" is enabled or not.
    /// </summary>
    /// <returns>True if the "laser" is enabled; otherwise, false.</returns>
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



