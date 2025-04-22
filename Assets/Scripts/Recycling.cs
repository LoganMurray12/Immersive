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

    [Header("Recycling Threshold")]
    public int requiredItems = 5;

    private bool isReady = false;
    private int recycleCount = 0;

    private void Start()
    {
        StartCoroutine(EnableRecyclingAfterDelay(startupDelay));
    }

    private IEnumerator EnableRecyclingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isReady) return;

        if (recyclableObjects.Contains(other.gameObject))
        {
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grab == null || !grab.isSelected)
            {
                Debug.Log("Accepted recyclable: " + other.name);
                StartCoroutine(RecycleItem(other.gameObject));
            }
        }
    }

    private IEnumerator RecycleItem(GameObject item)
    {
        yield return new WaitForSeconds(recycleDelay);

        Destroy(item);
        recycleCount++;
        Debug.Log("Recycle count: " + recycleCount);

        if (recycleCount >= requiredItems)
        {
            recycleCount = 0;

            if (eWastePrefab != null && outputSpawnPoint != null)
            {
                Instantiate(eWastePrefab, outputSpawnPoint.position, outputSpawnPoint.rotation);
                Debug.Log("E-waste created!");
            }
            else
            {
                Debug.LogWarning("Missing prefab or spawn point!");
            }
        }
    }
}
