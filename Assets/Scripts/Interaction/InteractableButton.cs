using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableButton : XRBaseInteractable
{
    public UnityEvent OnPress;

    private float yMin = 0f;
    private float yMax = 0f;
    private bool previousePress = false;

    private float previouseHandHeight = 0f;
    private XRBaseInteractor hoverInteractor;

    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    
    void Start()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.position.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.position.y;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previouseHandHeight - newHandHeight;
            previouseHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);
            CheckPress();
        }
    }

    void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previouseHandHeight = interactor.transform.position.y;
    }

    void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousePress = false;
        previouseHandHeight = 0f;
        SetYPosition(yMax);
    }

    void CheckPress()
    {
        bool inPosition = IsInPosition();

        if (inPosition && inPosition != previousePress)
        {
            OnPress.Invoke();
        }

        previousePress = inPosition;
    }

    void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    bool IsInPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        return transform.localPosition.y == inRange;
    }

    float GetLocalYPosition(Vector3 position)
    {
        return transform.root.InverseTransformPoint(position).y;
    }
}
