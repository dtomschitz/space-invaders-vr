using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHider : MonoBehaviour
{
    public GameObject handObject = null;

    private XRDirectInteractor interactor = null;

    void Awake()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    void OnEnable()
    {
        interactor.onSelectEntered.AddListener(Hide);
        interactor.onSelectExited.AddListener(Show);
    }

    void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(Hide);
        interactor.onSelectExited.RemoveListener(Show);
    }

    void Show(XRBaseInteractable interactable)
    {
        handObject.SetActive(true);
    }

    void Hide(XRBaseInteractable interactable)
    {
        handObject.SetActive(false);
    }
}
