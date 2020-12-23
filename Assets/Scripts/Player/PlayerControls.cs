
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


public class PlayerControls : MonoBehaviour
{
    public PlayerInputActions inputActions;

    public delegate void LeftTriggerButton(float value);
    public event LeftTriggerButton OnLeftTriggerButton;

    public delegate void LeftGripButton(float value);
    public event LeftGripButton OnLeftGripButton;

    public delegate void LeftTriggerButtonPressed();
    public event LeftTriggerButtonPressed OnLeftTriggerButtonPressed;

    public delegate void RightTriggerButton(float value);
    public event RightTriggerButton OnRightTriggerButton;

    public delegate void RightGripButton(float value);
    public event RightGripButton OnRightGripButton;

    public delegate void RightTriggerButtonPressed();
    public event RightTriggerButtonPressed OnRightTriggerButtonPressed;

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
        inputActions.LeftHand.Trigger.performed += ctx => OnLeftTriggerButton?.Invoke(ctx.ReadValue<float>());
        inputActions.LeftHand.Grip.performed += ctx => OnLeftGripButton?.Invoke(ctx.ReadValue<float>());
        inputActions.LeftHand.UIPress.performed += PauseGame;
    }

    void InitializeRightHandActions()
    {
        inputActions.RightHand.Activate.performed += ctx => OnRightTriggerButtonPressed?.Invoke();
        inputActions.RightHand.Trigger.performed += ctx => OnRightTriggerButton?.Invoke(ctx.ReadValue<float>());
        inputActions.RightHand.Grip.performed += ctx => OnRightGripButton?.Invoke(ctx.ReadValue<float>());
    }

    void PauseGame(CallbackContext ctx)
    {
        GameState.instance.SetState(GameStateType.GamePaused);
    }
}
