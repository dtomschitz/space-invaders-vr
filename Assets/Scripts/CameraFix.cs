using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    // set to be the parent of this object
    public Transform holder;

    // simple utility functions
    public static Quaternion LocalToWorldRotation(Transform t, Quaternion localRot)
    {
        return t.rotation * localRot;
    }
    public static Quaternion WorldToLocalRotation(Transform t, Quaternion rot)
    {
        return Quaternion.Inverse(t.rotation) * rot;
    }

    // Gets the body part position in world space
    public Vector3 GetBodyPartPosition(UnityEngine.XR.XRNode bodyPart)
    {
        return holder.localToWorldMatrix.MultiplyPoint(UnityEngine.XR.InputTracking.GetLocalPosition(bodyPart));
    }

    // Gets the body part rotation in world space
    public Quaternion GetBodyPartRotation(UnityEngine.XR.XRNode bodyPart)
    {
        return LocalToWorldRotation(holder, UnityEngine.XR.InputTracking.GetLocalRotation(bodyPart));
    }

    // moves entire player body so that this becomes true
    public void SetBodyPartPosition(UnityEngine.XR.XRNode bodyPart, Vector3 pos)
    {
        Vector3 bodyPartPos = holder.localToWorldMatrix.MultiplyPoint(UnityEngine.XR.InputTracking.GetLocalPosition(bodyPart));
        holder.position += pos - bodyPartPos;
    }


    // if you want to change rotation you should probably only use this to keep the headset tilted the same way (y axis is up, so 0 is forward along x axis, 180 is backwards along x axis, 90 and 270 are along z axis, etc.)
    public void SetBodyPartYRotation(UnityEngine.XR.XRNode bodyPart, float yRot)
    {
        Vector3 curRot = GetBodyPartRotation(bodyPart).eulerAngles;
        SetBodyPartRotation(bodyPart, new Vector3(curRot.x, yRot, curRot.z));
    }

    // rotates entire player body so that this becomes true (but doesn't change position)
    public void SetBodyPartRotation(UnityEngine.XR.XRNode bodyPart, Quaternion rot)
    {
        Quaternion curWorldRot = GetBodyPartRotation(bodyPart);
        Quaternion cancelOutWorldRot = Quaternion.Inverse(curWorldRot);
        float angle1;
        Vector3 axis1;
        cancelOutWorldRot.ToAngleAxis(out angle1, out axis1);
        Vector3 bodyPartPosition = GetBodyPartPosition(bodyPart);
        holder.RotateAround(bodyPartPosition, axis1, angle1);
        // now player rotation = not rotated
        float angle2;
        Vector3 axis2;
        rot.ToAngleAxis(out angle2, out axis2);
        holder.RotateAround(bodyPartPosition, axis2, angle2);
    }

    // rotates entire player body so that this becomes true (but doesn't change position)
    public void SetBodyPartRotation(UnityEngine.XR.XRNode bodyPart, Vector3 eulerAngles)
    {
        SetBodyPartRotation(bodyPart, Quaternion.Euler(eulerAngles));
    }
}