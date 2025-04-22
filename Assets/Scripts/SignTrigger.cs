using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstructionSign : MonoBehaviour
{
    public GameObject sign;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            sign.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            sign.SetActive(false);
        }
    }
}
