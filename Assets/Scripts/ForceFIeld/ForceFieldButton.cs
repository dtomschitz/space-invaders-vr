using UnityEngine;
using UnityEngine.Events;

public class ForceFieldButton : MonoBehaviour
{
    public event UnityAction OnButtonPress;
    public float pressLength;

    bool pressed;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float distance = Mathf.Abs(transform.position.y - startPosition.y);
        if (distance >= pressLength)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y - pressLength, transform.position.z);
            if (!pressed)
            {
                pressed = true;
                OnButtonPress?.Invoke();
            }
        } else
        {
            pressed = false;
        }

        if (transform.position.y > startPosition.y)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DWADda");    
    }
}

/*public class ForceFieldButton : MonoBehaviour
{
    public event Action OnButtonPress;

    float yMin = 0f;
    float yMax = 0f;
    bool previousePress = false;

    float previousHandHeight = 0f;
    XRBaseInteractor hoverInteractor;

    void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    void Start()
    {
        SetMinMax();
    }

     void OnDestroy()
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
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            bool inPosition = InPosition();
            if (inPosition && inPosition != previousePress)
            {
                OnButtonPress?.Invoke();
            }

            previousePress = inPosition;
        }
    }

    void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(interactor.transform.position);
        Player.instance.HideHands();
    }

    void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0f;
        previousePress = false;

        SetYPosition(yMax);
        Player.instance.ShowHands();
    }

    void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;
    }

    void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        return localPosition.y;
    }

    bool InPosition()
    {
        float range = Mathf.Clamp(transform.localPosition.y, yMin, yMax + 0.1f);
        return transform.localPosition.y == range;
    }
}
*/