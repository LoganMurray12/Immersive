using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RecyclingMachine : MonoBehaviour
{
    [Header("Recycling Output")]
    public Transform outputSpawnPoint;       // Reference to the output spawn point
    public GameObject eWastePrefab;          // E-waste cube prefab

    [Header("Recycle Settings")]
    public float recycleDelay = 1.5f;        // Delay before producing e-waste

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Recycle"))
        {
            StartCoroutine(RecycleItem(other.gameObject));
        }
    }

    private IEnumerator RecycleItem(GameObject item)
    {
        // Optional: Play a sound or effect here

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
