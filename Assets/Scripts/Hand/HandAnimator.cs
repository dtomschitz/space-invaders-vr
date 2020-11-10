using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    public float speed = 5.0f;
    public XRController controller;

    private Animator animator;

    private readonly List<Finger> gripFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky),
    };

    private readonly List<Finger> pointFingers = new List<Finger>()
    {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb),
    };

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            SetFingerTargets(gripFingers, gripValue);
        }

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            SetFingerTargets(pointFingers, triggerValue);
        }

        SmoothFinger(pointFingers);
        SmoothFinger(gripFingers);

        AnimateFingers(pointFingers);
        AnimateFingers(gripFingers);
    }

    void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach(Finger finger in fingers)
        {
            finger.target = value;
        }
    }

    void SmoothFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    void AnimateFingers(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            animator.SetFloat(finger.type.ToString(), finger.current);
        }
    }
}