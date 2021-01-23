using UnityEngine;
using UnityEngine.Events;

public class ForceFieldButton : MonoBehaviour
{
    public Material defaultMaterial;
    public Material highlightMaterial;

    public event UnityAction OnButtonPress;

    bool pressed;

    void OnTriggerEnter(Collider other)
    {
        if (IsEnabled && (other.tag == "LeftHandController" || other.tag == "RightHandController"))
        {
            GetComponent<Renderer>().material = pressed ? defaultMaterial : highlightMaterial;
            pressed = !pressed;
            OnButtonPress?.Invoke();
        }
    }

    public bool IsEnabled { set; get; } = false;
}