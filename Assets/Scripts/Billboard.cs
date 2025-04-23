using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main?.transform;
    }

    void Update()
    {
        if (cam == null) return;

        Vector3 direction = cam.position - transform.position;
        direction.y = 0f; // Keep upright
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
