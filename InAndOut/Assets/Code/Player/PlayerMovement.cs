using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Update is called once per frame
    void Update()
    {
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
        
        //Movement checks
        if (!movementLocked) //Only allow movement input while the movement IS NOT locked
        {
            
            //Walk forward
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 newPosition  = transform.position + -transform.right * speed * Time.deltaTime;

                rb.MovePosition(newPosition);
                camAnimator.SetBool("moving", true); //Set moving parameter to true if the player is moving

                playerState = PlayerState.Walking; //Set state to walking if player is moving

            }
            else
            { //Set moving parameter to false if the player stops moving
                camAnimator.SetBool("moving", false);

                playerState = PlayerState.Idle; //Set state to idle if player is idle
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
