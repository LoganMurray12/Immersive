using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverController : MonoBehaviour
{
    public Transform turbine; // Assign turbine GameObject in Inspector
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Assign in Inspector

    public Light redLight;  // Assign the red light in the Inspector
    public Light greenLight; // Assign the green light in the Inspector

    private bool isSpinning = false;

    private void Start()
    {
        // Add event listener for when the lever is grabbed
        grabInteractable.selectEntered.AddListener(OnLeverGrabbed);
        grabInteractable.selectExited.AddListener(OnLeverReleased);

        // Initialize light states
        UpdateLightStatus();
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
                UpdateLightStatus();
            }
        }
        else if (angle < 250 || angle > 290) // When pushed back up
        {
            if (isSpinning)
            {
                isSpinning = false;
                Debug.Log("Lever Up! Turbine Deactivated.");
                UpdateLightStatus();
            }
        }

        // Apply rotation if turbine is active 
        if (isSpinning)
        {
            turbine.Rotate(Vector3.back * 100 * Time.deltaTime);
        }
    }

    private void UpdateLightStatus()
    {
        if (isSpinning)
        {
            greenLight.enabled = true;
            redLight.enabled = false;
            Debug.Log("Green Light on");
        }
        else
        {
            greenLight.enabled = false;
            redLight.enabled = true;
            Debug.Log("Red Light on");
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
