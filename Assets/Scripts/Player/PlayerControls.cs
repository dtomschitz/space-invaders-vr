using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


public class PlayerControls : MonoBehaviour
{
    public PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.LeftHand.Activate.performed += TriggerButtonAction;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }


    void TriggerButtonAction(CallbackContext ctx) 
    {
        Debug.Log("SHooT");
    }

    void PauseGame(CallbackContext ctx)
    {
        GameState.instance.SetState(GameStateType.GamePaused);
    }
}
