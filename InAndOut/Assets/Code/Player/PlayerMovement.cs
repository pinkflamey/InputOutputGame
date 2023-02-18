using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationDuration = 3f;


    public bool movementLocked = false;

    private Rigidbody rb;
    private Animator camAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Movement checks
        if (!movementLocked) //Only allow movement input while the movement IS NOT locked
        {
            
            //Walk forward
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 newPosition  = transform.position + -transform.right * speed * Time.deltaTime;

                rb.MovePosition(newPosition);
                camAnimator.SetBool("moving", true); //Set moving parameter to true if the player is moving
            }
            else
            { //Set moving parameter to false if the player stops moving
                camAnimator.SetBool("moving", false);
            }

            //Rotate left (-90 degrees)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Rotate(-90);
            }
        
            //Rotate right (+90 degrees)
            if (Input.GetKeyDown(KeyCode.E))
            {
                Rotate(90);
            }
            
        }
        else
        { //Set moving parameter to false if the movement gets locked
            camAnimator.SetBool("moving", false);
        }

    }

    void Rotate(float degrees)
    {
        Hashtable hash = iTween.Hash
            (
            "amount", new Vector3(0, degrees, 0), //Amount to rotate
            "time", rotationDuration, //Time for the rotation to take
            "onstarttarget", gameObject,
            "onstart", "SetMovementState", //Lock movement
            "oncompletetarget", gameObject,
            "oncomplete", "SetMovementState", //Unlock movement
            "easetype", iTween.EaseType.linear //Rotation is linear
            );

        iTween.RotateAdd(gameObject, hash);

    }

    void SetMovementState()
    {
        movementLocked = !movementLocked;
    }
}
