using UnityEngine;

public class OutputCamera : MonoBehaviour
{

    public Camera outputCamera;

    void Start()
    {
        outputCamera = GetComponent<Camera>();
        outputCamera.stereoTargetEye = StereoTargetEyeMask.None;
    }
}
