using System.Collections.Generic;
using UnityEngine;

public enum HandPosition
{
    LEFT,
    RIGHT
}

public class Hand : MonoBehaviour
{
    public float speed = 5.0f;
    public HandPosition position;

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

    void Start()
    {
        if (position == HandPosition.LEFT)
        {
            Player.instance.controls.OnLeftTriggerButton += value => SetFingerTargets(pointFingers, value);
            Player.instance.controls.OnLeftGripButton += value => SetFingerTargets(gripFingers, value);
        } else
        {
            Player.instance.controls.OnRightTriggerButton += value => SetFingerTargets(pointFingers, value);
            Player.instance.controls.OnRightGripButton += value => SetFingerTargets(gripFingers, value);
        }
    }

    void Update()
    {
        SmoothFinger(pointFingers);
        SmoothFinger(gripFingers);

        AnimateFingers(pointFingers);
        AnimateFingers(gripFingers);
    }

    void SetFingerTargets(List<Finger> fingers, float value)
    {

        foreach (Finger finger in fingers)
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

    public void EnableHand(bool value)
    {
        gameObject.SetActive(value);
    }
}
