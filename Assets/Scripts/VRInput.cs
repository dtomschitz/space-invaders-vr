using UnityEngine;
using UnityEngine.EventSystems;

public class VRInput : BaseInput
{
    public Camera eventCamera;

    protected override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;
    }
}
