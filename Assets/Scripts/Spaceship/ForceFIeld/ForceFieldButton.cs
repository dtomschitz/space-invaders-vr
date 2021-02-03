using UnityEngine;
using UnityEngine.Events;

public class ForceFieldButton : MonoBehaviour
{
    public Material defaultMaterial;
    public Material highlightMaterial;

    public event UnityAction OnButtonPress;

    bool pressed;

    void Start()
    {
        ForceField.instance.OnForceFieldPowerConsumed += () => ToggleButton(false);
        GameState.instance.OnGameStateChanged += (state) =>
        {
            if (state == GameStateType.PauseMenu) ToggleButton(false);
        };
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsEnabled && (other.CompareTag("LeftHandController") || other.CompareTag("RightHandController")))
        {
            ToggleButton(!pressed);
            OnButtonPress?.Invoke();
        }
    }

    void ToggleButton(bool value)
    {
        pressed = value;
        GetComponent<Renderer>().material = pressed ? highlightMaterial : defaultMaterial;
    }

    public bool IsEnabled { set; get; } = false;
}