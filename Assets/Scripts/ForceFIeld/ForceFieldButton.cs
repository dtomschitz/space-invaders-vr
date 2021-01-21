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
        if (other.tag == "LeftHandController" || other.tag == "RightHandController")
        {
            GetComponent<Renderer>().material = pressed ? defaultMaterial : highlightMaterial;
            pressed = !pressed;
            if (pressed) OnButtonPress?.Invoke();
        }
    }
}