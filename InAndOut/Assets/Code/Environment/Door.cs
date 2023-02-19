using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool trigger = false;

    public bool isOpen = false;

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            trigger = !trigger;

            if (isOpen)
            {
                //Close
                animator.SetTrigger("Close");
                isOpen = !isOpen;
            }
            else
            {
                //Open
                animator.SetTrigger("Open");
                isOpen = !isOpen;
            }
        }
    }
}
