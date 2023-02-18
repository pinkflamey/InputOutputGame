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
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Debug.Log("function running");

        movementLocked = !movementLocked;

        /*if (!movementLocked) //If the movement IS NOT locked
        {
            movementLocked = true; //Lock the movement
        }
        else if (movementLocked) //If the movement IS locked
        {
            movementLocked = false; //Unlock the movement
        }*/
    }
}
