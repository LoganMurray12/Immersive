using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRenderer : MonoBehaviour
{
    public Transform[] controlPoints;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = controlPoints.Length;
    }

    void LateUpdate()
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, controlPoints[i].position);
        }
    }
}
