using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverController : MonoBehaviour
{
    public Transform turbine; // Assign turbine GameObject in Inspector
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Assign in Inspector
    private bool isSpinning = false;

    private void Start()
    {
        // Add event listener for when the lever is grabbed
        grabInteractable.selectEntered.AddListener(OnLeverGrabbed);
        grabInteractable.selectExited.AddListener(OnLeverReleased);
    }

    private void Update()
    {
        // Check the lever's angle to determine if the turbine should be active
        float angle = transform.localEulerAngles.z;

        // Ensure correct angle detection (adjust based on hinge joint axis)
        if (angle > 250 && angle < 290) // Adjust for your lever's rotation
        {
            if (!isSpinning)
            {
                isSpinning = true;
                Debug.Log("Lever Down! Turbine Activated.");
            }
        }
        else if (angle < 250 || angle > 290) // When pushed back up
        {
            if (isSpinning)
            {
                isSpinning = false;
                Debug.Log("Lever Up! Turbine Deactivated.");
            }
        }

        // Apply rotation if turbine is active 
        if (isSpinning)
        {
            turbine.Rotate(Vector3.forward * 100 * Time.deltaTime);
        }
    }

    private void OnLeverGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Lever Grabbed!");
    }

    private void OnLeverReleased(SelectExitEventArgs args)
    {
        Debug.Log("Lever Released!");
    }
}
