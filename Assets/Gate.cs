using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        OpenGate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenGate()
    {
        Animator.SetBool("open", true);
    }

    private void CloseGate()
    {
        Animator.SetBool("open", false);
    }
}
