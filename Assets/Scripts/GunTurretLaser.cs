using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretLaser : MonoBehaviour
{

    public float defaultLength = 3.0f;
    public GameObject hand;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, CalculateEnd());
    }

    private Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRaycast();
        Vector3 position = transform.position + (this.hand.transform.forward * defaultLength);

        if (hit.collider)
        {
            position = hit.point;
        }

        return position;
    }

    private RaycastHit CreateForwardRaycast()
    {
        Ray ray = new Ray(transform.position, this.hand.transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, defaultLength);

        return hit;
    }
}



