using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugSocket : MonoBehaviour
{
    public string requiredTag = "WireEnd";
    private bool isPlugged = false;
    public float snapSpeed = 10f;

    private Transform plugToSnap;

    [Header("Audio")]
    public AudioSource plugInSound; // Assign in Inspector (zap sound)

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag) && !isPlugged)
        {
            plugToSnap = other.transform;

            // Disable grabbing
            var grabInteractable = plugToSnap.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }

            // Play sound
            if (plugInSound != null)
            {
                plugInSound.Play();
            }

            isPlugged = true;
        }
    }

    void Update()
    {
        if (isPlugged && plugToSnap != null)
        {
            plugToSnap.position = Vector3.Lerp(plugToSnap.position, transform.position, Time.deltaTime * snapSpeed);
            plugToSnap.rotation = Quaternion.Slerp(plugToSnap.rotation, transform.rotation, Time.deltaTime * snapSpeed);
        }
    }
}
