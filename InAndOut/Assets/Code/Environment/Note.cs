using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField] private GameObject canvasNote;
    [SerializeField] [TextArea] private string text;
    [SerializeField] private bool isKeyCode;

    [Space]
    
    public bool isOpened;
    
    [SerializeField] private bool openNote = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openNote)
        {
            openNote = false;
            OpenNote();
        }
    }

    public void OpenNote()
    {
        if (!isOpened)
        {
            if (isKeyCode)
            {
                GameObject.Find("Terminal").GetComponent<KeycodeTerminal>().SetHasCode(true);
            }
            isOpened = true;
            GameObject canvas = Instantiate(canvasNote);
            canvas.GetComponent<NoteCanvas>().openedNote = GetComponent<Note>();
            canvas.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = text;
            GameObject.Find("Player").GetComponent<PlayerMovement>().SetMovementLockState(true);
        }
    }
}
