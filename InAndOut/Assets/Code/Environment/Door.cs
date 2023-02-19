using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool trigger = false;
    [SerializeField] private bool triggerLock = false;

    public bool isOpen = false;
    public bool isLocked = false;

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger) //When triggered;
        {
            trigger = !trigger; //Reset the trigger

            if (isOpen) //If the door is open;
            {
                animator.SetTrigger("Close"); //Close it
                isOpen = !isOpen; //Set isOpen to false (door is closed)
            }
            else if(!isOpen && !isLocked) //If the door is closed and not locked;
            {
                animator.SetTrigger("Open"); //Open the door
                isOpen = !isOpen; //Set isOpen to true (door is open)
            }
            else if (!isOpen && isLocked) //If the door is closed but locked;
            {
                Debug.Log("Door is locked!"); //Door is locked
            }
        }

        if (triggerLock)
        {
            triggerLock = !triggerLock;
            isLocked = !isLocked;
        }
    }

    public void TriggerDoor()
    {
        trigger = true;
    }

    public void TriggerDoorLock()
    {
        triggerLock = true;
    }
}
