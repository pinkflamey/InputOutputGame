using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Walking
    }

    [SerializeField] private PlayerState playerState;
    
    [Space]
    
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationDuration = 3f;
    [SerializeField] private CalculateMonsterDest hrDestCalc;

    [SerializeField] private bool movementLocked = false;
    [SerializeField] private bool moving = false;
    [SerializeField] private InputAction actionForward;


    private Rigidbody rb;
    private Animator camAnimator;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camAnimator = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponents<AudioSource>()[1];
        
        
    }

    private void FixedUpdate()
    {
        /*
         * Movement check to move player if moving, in FixedUpdate to prevent speed irregularities
         */
        
        if (moving && !movementLocked) //If moving & movement is not locked
        {
            //Position to walk to
            Vector3 newPosition  = transform.position + -transform.right * speed * Time.deltaTime;
            
            rb.MovePosition(newPosition); //Move player
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Audio
         */
        
        if (playerState == PlayerState.Walking)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (playerState == PlayerState.Idle)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        
        /*
         * Movement checks
         *   If moving and movement not locked, move player
         *   Sets camera animation to false if the player is not moving (or if movement is locked)
         *   Sets state to Idle if player is not moving (or if movement is locked)
         */

        if (movementLocked) //If the player can't move
        {
            playerState = PlayerState.Idle; //Set state to idle
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        /*
         * Script runs when Input System in Player Input component receives moving input
         */
        
        if (context.started)
        {
            moving = true;
            
            camAnimator.SetBool("moving", true); //Set moving parameter to true
            playerState = PlayerState.Walking; //Set state to walking
        }
        else if (context.canceled)
        {
            moving = false;
            
            camAnimator.SetBool("moving", false); //Set moving parameter to false if the player stops moving
            playerState = PlayerState.Idle; //Set state to idle
        }
    }

    public void Rotate(float degrees)
    {
        /*
         * Script runs when Input System in Player Input component receives rotation input
         */
        
        if (!movementLocked) //If movement is not locked
        {
            Hashtable hash = iTween.Hash
            (
                "amount", new Vector3(0, degrees, 0), //Amount to rotate
                "time", rotationDuration, //Time for the rotation to take
                "onstarttarget", gameObject,
                "onstart", "SetMovementLockState", //Lock movement
                "oncompletetarget", gameObject,
                "oncomplete", "SetMovementLockState", //Unlock movement
                "easetype", iTween.EaseType.linear //Rotation is linear
            );

            //Rotate player
            iTween.RotateAdd(gameObject, hash);
        }
    }

    public void SetMovementLockState(bool state)
    {
        movementLocked = state;
    }
}
