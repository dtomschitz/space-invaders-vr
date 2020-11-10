using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    public float smoothing = 15.0f;
    public Transform target;

    private Rigidbody rigidBody;
    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        TeleportToTarget();
    }

    void Update()
    {
        SetTargetPosition();
        SetTargetRotation();
    }

    void FixedUpdate()
    {
        MoveToController();
        RotateToController();
    }

    void SetTargetPosition()
    {
        float time = smoothing * Time.unscaledDeltaTime;
        targetPosition = Vector3.Lerp(targetPosition, target.position, time);
    }

    void SetTargetRotation()
    {
        float time = smoothing * Time.unscaledDeltaTime;
        targetRotation = Quaternion.Slerp(targetRotation, target.rotation, time);
    }

    void MoveToController()
    {
        Vector3 positionDelta = targetPosition - transform.position;
        rigidBody.velocity = Vector3.zero;
        rigidBody.MovePosition(transform.position + positionDelta);

    }

    void RotateToController()
    {
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.MoveRotation(targetRotation);
    }

    public void TeleportToTarget()
    {
        targetPosition = target.position;
        targetRotation = target.rotation;
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
