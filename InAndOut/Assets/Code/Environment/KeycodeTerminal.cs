using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;

public class KeycodeTerminal : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private float beepDelay;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    [Space]
    
    [SerializeField] private GameObject keycodeCanvas;

    [SerializeField] private PlayerMovement pm;
    
    //------------

    [SerializeField] private bool hasCode = false;
    [SerializeField] private bool run = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            run = false;
            StartCoroutine(StartCodeEnter());
        }
    }

    public IEnumerator StartCodeEnter()
    {
        if (hasCode)
        {
            //Enable the canvas
            GameObject canvas = Instantiate(keycodeCanvas);
        
            //Disable player controls
            pm.SetMovementLockState(true);

            //Wait
            yield return new WaitForSeconds(beepDelay);
            //Play sound
            audioSource.PlayOneShot(clip);
            //Wait
            yield return new WaitForSeconds(beepDelay);
            //Play sound
            audioSource.PlayOneShot(clip);
            //Wait
            yield return new WaitForSeconds(beepDelay);
            //Play sound
            audioSource.PlayOneShot(clip);
            //Wait
            yield return new WaitForSeconds(beepDelay);
            //Play sound
            audioSource.PlayOneShot(clip);

            //Wait
            yield return new WaitForSeconds(beepDelay * 2);

            //Disable canvas
            Destroy(canvas);
            //Enable player controls
            pm.SetMovementLockState(false);
            //Open door
            door.TriggerDoorLock();
            //Wait
            yield return new WaitForSeconds(1f);
            //Open door
            door.TriggerDoor();
        }
        else
        {
            yield return null;
        }
    }
}
