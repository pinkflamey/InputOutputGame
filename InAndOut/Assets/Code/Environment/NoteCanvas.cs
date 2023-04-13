using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteCanvas : MonoBehaviour
{
    private bool canClose;

    public Note openedNote;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StayTimer());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator StayTimer()
    {
        canClose = false;
        yield return new WaitForSeconds(2f);
        canClose = true;
    }

    public void OnAction()
    {
        if (canClose)
        {
            Debug.Log("Started");
            GameObject.Find("Player").GetComponent<PlayerMovement>().SetMovementLockState(false);
            openedNote.isOpened = false;
            
            Destroy(gameObject);
        }
    }
}
