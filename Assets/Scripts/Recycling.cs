using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecyclingMachine : MonoBehaviour
{
    [Header("Recycling Output")]
    public Transform outputSpawnPoint;
    public GameObject eWastePrefab;

    [Header("Recycle Settings")]
    public float recycleDelay = 1.5f;
    public float startupDelay = 1f;

    [Header("Valid Recyclables")]
    public List<GameObject> recyclableObjects = new List<GameObject>();

    [Header("Recycling Sound")]
    public AudioSource recycleSound;

    private bool isReady = false;

    // Track objects we've already processed
    private HashSet<GameObject> alreadyRecycled = new HashSet<GameObject>();

    private void Start()
    {
        StartCoroutine(EnableRecyclingAfterDelay(startupDelay));
    }

    private IEnumerator EnableRecyclingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReady = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isReady) return;

        GameObject obj = other.gameObject;

        if (recyclableObjects.Contains(obj) && !alreadyRecycled.Contains(obj))
        {
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grab == null || !grab.isSelected) // Only recycle when not being held
            {
                alreadyRecycled.Add(obj); // Prevent multiple triggers
                Debug.Log("Accepted recyclable (via Stay): " + obj.name);
                StartCoroutine(RecycleItem(obj));
            }
        }
    }

    private IEnumerator RecycleItem(GameObject item)
    {
        // Play sound immediately
        if (recycleSound != null)
        {
            recycleSound.Play();
        }

        // Destroy item immediately
        Destroy(item);

        // Wait before spawning e-waste
        yield return new WaitForSeconds(recycleDelay);

        if (eWastePrefab != null && outputSpawnPoint != null)
        {
            Instantiate(eWastePrefab, outputSpawnPoint.position, outputSpawnPoint.rotation);
            Debug.Log("E-waste created!");
        }
        else
        {
            Debug.LogWarning("Missing prefab or spawn point!");
        }

        // Optional: Clean up the record for memory safety (not necessary if objects are destroyed)
        alreadyRecycled.Remove(item);
    }
}
