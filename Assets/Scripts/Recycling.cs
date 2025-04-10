using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RecyclingMachine : MonoBehaviour
{
    [Header("Recycling Output")]
    public Transform outputSpawnPoint;       // Where the e-waste will appear
    public GameObject eWastePrefab;          // Your Blender e-waste prefab

    [Header("Recycle Settings")]
    public float recycleDelay = 1.5f;        // Delay before producing e-waste
    public float startupDelay = 1f;          // Time after start before recycling can happen

    private bool isReady = false;

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

        if (other.CompareTag("Recycle"))
        {
            StartCoroutine(RecycleItem(other.gameObject));
        }
    }

    private IEnumerator RecycleItem(GameObject item)
    {
        // Optional: Insert sound or VFX here
        yield return new WaitForSeconds(recycleDelay);

        Destroy(item);

        if (eWastePrefab != null && outputSpawnPoint != null)
        {
            Instantiate(eWastePrefab, outputSpawnPoint.position, outputSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Missing prefab or spawn point on RecyclingMachine script!");
        }
    }
}
