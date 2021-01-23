using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRManager : MonoBehaviour
{
    #region Singelton

    public static XRManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public XRInteractorLineVisual leftInteractor;
    public XRInteractorLineVisual rightInteractor;

    public void EnableXRInteractors(bool value)
    {
        leftInteractor.enabled = value;
        rightInteractor.enabled = value;
    }
}
