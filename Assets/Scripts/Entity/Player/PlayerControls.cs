using UnityEngine;

/// <summary>
/// Class <c>PlayerControls</c> subscribes to specific input events in order to
/// provide them for every script which needs information about the different
/// input actions the player can perform.
/// </summary>
public class PlayerControls : MonoBehaviour
{
    public PlayerInputActions inputActions;

    public delegate void LeftTriggerButton(float value);
    public event LeftTriggerButton OnLeftTriggerButton;

    public delegate void LeftTriggerButtonPressed();
    public event LeftTriggerButtonPressed OnLeftTriggerButtonPressed;

    public delegate void LeftTriggerButtonPressCanceled();
    public event LeftTriggerButtonPressCanceled OnLeftTriggerButtonPressCanceled;

    public delegate void LeftGripButton(float value);
    public event LeftGripButton OnLeftGripButton;

    public delegate void RightTriggerButton(float value);
    public event RightTriggerButton OnRightTriggerButton;

    public delegate void RightGripButton(float value);
    public event RightGripButton OnRightGripButton;

    public delegate void RightTriggerButtonPressed();
    public event RightTriggerButtonPressed OnRightTriggerButtonPressed;

    public delegate void RightTriggerButtonPressCanceled();
    public event RightTriggerButtonPressCanceled OnRightTriggerButtonPressCanceled;

    public delegate void PausedButtonPressed();
    public event PausedButtonPressed OnPausedButtonPressed;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        InitializeLeftHandActions();
        InitializeRightHandActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void InitializeLeftHandActions()
    {
        inputActions.LeftHand.Activate.performed += ctx => OnLeftTriggerButtonPressed?.Invoke();
        inputActions.LeftHand.Activate.canceled += ctx => OnLeftTriggerButtonPressCanceled?.Invoke();

        inputActions.LeftHand.Trigger.performed += ctx => OnLeftTriggerButton?.Invoke(ctx.ReadValue<float>());
        inputActions.LeftHand.Grip.performed += ctx => OnLeftGripButton?.Invoke(ctx.ReadValue<float>());
        inputActions.LeftHand.Pause.performed += ctx => OnPausedButtonPressed?.Invoke();
    }

    void InitializeRightHandActions()
    {
        inputActions.RightHand.Activate.performed += ctx => OnRightTriggerButtonPressed?.Invoke();
        inputActions.RightHand.Activate.canceled += ctx => OnRightTriggerButtonPressCanceled?.Invoke();


        inputActions.RightHand.Trigger.performed += ctx => OnRightTriggerButton?.Invoke(ctx.ReadValue<float>());
        inputActions.RightHand.Grip.performed += ctx => OnRightGripButton?.Invoke(ctx.ReadValue<float>());
    }
}

