using UnityEngine;

public class OutputCamera : MonoBehaviour
{
    public int depth;
    public Camera outputCamera;
    public StereoTargetEyeMask stereoTargetEyeMask;

    void Start()
    {
        outputCamera = GetComponent<Camera>();
        outputCamera.stereoTargetEye = stereoTargetEyeMask;

        //outputCamera.depth = depth;
        UnityEngine.XR.XRSettings.showDeviceView = false;
    }
}
